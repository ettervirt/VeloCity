using MediatR;
using VeloCity.Api.Features.Tickets.Queries.GetActiveTicketTypes;

namespace VeloCity.Api.Features.Tickets.Queries.GetTicketType;

public record GetTicketTypeQuery(int Id) : IRequest<TicketTypeDto>;
