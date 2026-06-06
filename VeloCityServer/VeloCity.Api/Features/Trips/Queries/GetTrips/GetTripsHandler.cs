using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Trips.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Trips.Queries.GetTrips;

public class GetTripsHandler(ApplicationDbContext context)
    : IRequestHandler<GetTripsQuery, PaginatedList<TripDto>>
{
    public async Task<PaginatedList<TripDto>> Handle(GetTripsQuery request, CancellationToken ct)
    {
        var query = context.Trips
            .Include(t => t.Line)
            .AsNoTracking();
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(l => l.Line.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }
        query = query.OrderByDescending(t => t.Date);
        var dtoQuery = query.Select(t => new TripDto(
            t.Id,
            t.LineId,
            t.Line.Name,
            t.VehicleId,
            t.DriverId,
            t.IsActive,
            t.Date,
            t.Status.ToString()
        ));
        return await PaginatedList<TripDto>.CreateAsync(
            dtoQuery,
            request.PageNumber,
            request.PageSize,
            ct);
    }
}
