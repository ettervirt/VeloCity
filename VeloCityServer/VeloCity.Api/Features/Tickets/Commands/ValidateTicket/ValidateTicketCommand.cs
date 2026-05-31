using MediatR;

namespace VeloCity.Api.Features.Tickets.Commands.ValidateTicket;

public record ValidateTicketCommand(int TicketId, int VehicleId) : IRequest;
