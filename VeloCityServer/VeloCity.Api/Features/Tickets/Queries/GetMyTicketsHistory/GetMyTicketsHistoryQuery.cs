using MediatR;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Tickets.DTOs;

namespace VeloCity.Api.Features.Tickets.Queries.GetMyTicketsHistory;

public class GetMyTicketsHistoryQuery : PaginatedRequest,
    IRequest<PaginatedList<TicketDto>> {}
