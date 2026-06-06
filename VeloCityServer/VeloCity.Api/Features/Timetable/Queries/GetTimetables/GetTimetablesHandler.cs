using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Timetable.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Timetable.Queries.GetTimetables
{
    public class GetTimetablesHandler(ApplicationDbContext context)
        :IRequestHandler<GetTimetablesQuery, PaginatedList<TimetableDto>>
    {
        public async Task<PaginatedList<TimetableDto>> Handle(GetTimetablesQuery request, CancellationToken ct)
        {
            var query = context.Timetables
            .Include(t => t.Stop)
            .Include(t => t.Trip)
            .AsNoTracking();

            query = query.Where(t => t.Trip.IsActive);
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(t => t.Stop.Name.Contains(request.SearchTerm));
            }
            query = query.OrderByDescending(t => t.TripId);
               
            var dtoQuery = query.Select(t => new TimetableDto(
                t.Id,
                t.TripId,
                t.StopId,
                t.Stop.Name,
                t.Sequence,
                t.DepartureTime
            ));
            return await PaginatedList<TimetableDto>.CreateAsync(dtoQuery, request.PageNumber, request.PageSize, ct);
        }
    }
}
