using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Commands.ValidateTicket;

public class ValidateTicketHandler(
    ApplicationDbContext context,
    IUserContext userContext) : IRequestHandler<ValidateTicketCommand>
{
    public async Task Handle(ValidateTicketCommand request, CancellationToken ct)
    {
        var userId = userContext.Id
                     ?? throw new AppException("Unauthorized access.", 401);

        var ticket = await context.Tickets
                         .Include(t => t.TicketType)
                         .FirstOrDefaultAsync(t => t.Id == request.TicketId, ct)
                     ?? throw new NotFoundException("Ticket", request.TicketId);

        if (ticket.UserId != userId)
            throw new AppException("You do not own this ticket.", 403);

        if (ticket.IsValidated)
            throw new AppException("This ticket has already been validated.", 400);

        var vehicleExists = await context.Vehicles.AnyAsync(v => v.Id == request.VehicleId && v.IsActive, ct);
        if (!vehicleExists)
            throw new AppException("Invalid or inactive vehicle.", 400);

        var now = DateTime.UtcNow;
        ticket.IsValidated = true;

        ticket.VehicleId = request.VehicleId;
        ticket.ValidFrom = now;

        if (ticket.TicketType.DurationInMinutes > 0)
        {
            ticket.ValidTo = now.AddMinutes(ticket.TicketType.DurationInMinutes);
        }

        await context.SaveChangesAsync(ct);
    }
}
