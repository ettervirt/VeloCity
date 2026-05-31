using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Features.Tickets.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Queries.VerifyTicket;

public class VerifyTicketHandler(
    ApplicationDbContext context)
    : IRequestHandler<VerifyTicketQuery, TicketVerificationResultDto>
{
    public async Task<TicketVerificationResultDto> Handle(VerifyTicketQuery request,
        CancellationToken ct)
    {
        var ticket = await context.Tickets
            .AsNoTracking()
            .Include(t => t.TicketType)
            .FirstOrDefaultAsync(t => t.Id == request.TicketId,
                ct);


        if (ticket == null)
            return new TicketVerificationResultDto(false,
                "Ticket not found or fake.",
                null,
                null);


        if (!ticket.IsValidated)
            return new TicketVerificationResultDto(false,
                "Ticket is not validated.",
                ticket.TicketType.Name,
                null);


        if (ticket.VehicleId != request.VehicleId)
            return new TicketVerificationResultDto(
                false,
                $"Ticket validated in a different vehicle (ID: {ticket.VehicleId}).",
                ticket.TicketType.Name,
                ticket.ValidTo);


        if (ticket.ValidTo.HasValue && ticket.ValidTo.Value < DateTime.UtcNow)
            return new TicketVerificationResultDto(false,
                "Ticket has expired.",
                ticket.TicketType.Name,
                ticket.ValidTo);

        return new TicketVerificationResultDto(true,
            "Ticket is VALID.",
            ticket.TicketType.Name,
            ticket.ValidTo);
    }
}
