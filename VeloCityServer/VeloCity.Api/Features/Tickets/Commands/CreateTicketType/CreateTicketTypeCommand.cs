using MediatR;

namespace VeloCity.Api.Features.Tickets.Commands.CreateTicketType;

public record CreateTicketTypeCommand(
    string Name,
    decimal Price,
    int DurationInMinutes,
    int ZoneLimit) : IRequest<int>;
