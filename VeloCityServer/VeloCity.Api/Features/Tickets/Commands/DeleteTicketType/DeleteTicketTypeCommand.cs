using MediatR;

namespace VeloCity.Api.Features.Tickets.Commands.DeleteTicketType
{
    public record DeleteTicketTypeCommand(int Id) : IRequest;
}
