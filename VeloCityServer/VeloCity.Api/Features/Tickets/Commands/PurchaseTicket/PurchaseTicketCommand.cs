using MediatR;

namespace VeloCity.Api.Features.Tickets.Commands.PurchaseTicket;

public record PurchaseTicketCommand(
    int TicketTypeId) : IRequest<int>;
