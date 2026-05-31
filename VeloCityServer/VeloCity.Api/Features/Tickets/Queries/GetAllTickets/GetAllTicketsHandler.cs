using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Tickets.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Queries.GetAllTickets;

public class GetAllTicketsHandler(ApplicationDbContext context)
    : IRequestHandler<GetAllTicketsQuery, PaginatedList<TicketDto>>
{
    public async Task<PaginatedList<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken ct)
    {
        var query = context.Tickets.AsNoTracking().AsQueryable();


        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.VehicleId.HasValue)
        {
            query = query.Where(t => t.VehicleId == request.VehicleId.Value);
        }

        if (request.IsValidated.HasValue)
        {
            query = query.Where(t => t.IsValidated == request.IsValidated.Value);
        }

        query = query.OrderByDescending(t => t.PurchasedAt);


        var projectedQuery = query.Select(t => new TicketDto(
            t.Id,
            t.TicketType.Name,
            t.Price,
            t.PurchasedAt,
            t.ValidFrom,
            t.ValidTo,
            t.VehicleId,
            t.IsValidated));

        return await PaginatedList<TicketDto>.CreateAsync(projectedQuery, request.PageNumber, request.PageSize, ct);
    }
}
