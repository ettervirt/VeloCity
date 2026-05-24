using MediatR;

namespace VeloCity.Api.Features.Tickets.Queries.GetActiveTicketTypes;

public record GetActiveTicketTypesQuery : IRequest<List<TicketTypeDto>>;

public record TicketTypeDto(
    int Id,
    string Name,
    decimal Price,
    int DurationInMinutes,
    int ZoneLimit);
