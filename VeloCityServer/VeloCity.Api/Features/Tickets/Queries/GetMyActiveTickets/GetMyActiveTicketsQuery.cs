using MediatR;
using VeloCity.Api.Features.Tickets.DTOs;

namespace VeloCity.Api.Features.Tickets.Queries.GetMyActiveTickets;

public record GetMyActiveTicketsQuery : IRequest<List<TicketDto>>;
