using MediatR;

namespace VeloCity.Api.Features.Tickets.Commands.UpdateTicketType;

public record UpdateTicketTypeCommand(
    int Id,
    string Name,
    decimal Price,
    int DurationInMinutes,
    int ZoneLimit) : IRequest;
