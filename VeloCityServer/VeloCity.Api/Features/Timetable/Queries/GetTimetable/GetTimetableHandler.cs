using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Timetable.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Timetable.Queries.GetTimetable;

public class GetTimetableHandler(ApplicationDbContext context)
    : IRequestHandler<GetTimetableQuery, TimetableDto>
{
    public async Task<TimetableDto> Handle(GetTimetableQuery request, CancellationToken ct)
    {
        var timetable = await context.Timetables
            .Include(t => t.Stop)
            .AsNoTracking()
            .Where(t => t.Id == request.Id)
            .Select(t => new TimetableDto(
                t.Id,
                t.TripId,
                t.StopId,
                t.Stop.Name,
                t.Sequence,
                t.DepartureTime
            ))
            .FirstOrDefaultAsync(ct)
            ?? throw new NotFoundException("Timetable", request.Id);
        return timetable;
    }
}
