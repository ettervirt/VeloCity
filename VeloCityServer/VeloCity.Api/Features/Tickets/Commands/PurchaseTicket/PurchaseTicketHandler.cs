using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Commands.PurchaseTicket;

public class PurchaseTicketHandler(
    ApplicationDbContext context,
    IUserContext userContext) : IRequestHandler<PurchaseTicketCommand, int>
{
    public async Task<int> Handle(PurchaseTicketCommand request, CancellationToken ct)
    {
        var userId = userContext.Id ?? throw new AppException("Unauthorized access.", 401);

        var user = await context.Users.FindAsync([userId], ct)
                   ?? throw new AppException("User not found.", 404);

        var ticketType = await context.TicketTypes.FindAsync([request.TicketTypeId], ct)
                         ?? throw new NotFoundException("TicketType", request.TicketTypeId);

        if (!ticketType.IsActive)
            throw new AppException("This ticket type is no longer available.");

        if (user.Balance < ticketType.Price)
            throw new AppException("Insufficient funds. Please top up your wallet.");

        user.Balance -= ticketType.Price;

        var ticket = new Ticket
        {
            UserId = userId,
            TicketTypeId = ticketType.Id,
            Price = ticketType.Price,
            PurchasedAt = DateTime.UtcNow,
            IsValidated = false
        };

        context.Tickets.Add(ticket);
        await context.SaveChangesAsync(ct);

        return ticket.Id;
    }
}
