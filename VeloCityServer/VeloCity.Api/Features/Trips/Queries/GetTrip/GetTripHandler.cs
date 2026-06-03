using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Trips.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Trips.Queries.GetTrip;

public class GetTripHandler(ApplicationDbContext context)
    : IRequestHandler<GetTripQuery, TripDto>
{
    public async Task<TripDto> Handle(GetTripQuery request, CancellationToken ct)
    {
        var tripDto = await context.Trips
            .Include(t => t.Line)
            .AsNoTracking()
            .Where(t => t.Id == request.Id)
            .Select(t => new TripDto(
                t.Id,
                t.LineId,
                t.Line.Name,
                t.VehicleId,
                t.DriverId,
                t.IsActive,
                t.Date,
                t.Status.ToString()
            ))
            .FirstOrDefaultAsync(ct)
            ?? throw new NotFoundException("Trip", request.Id);
        return tripDto;
    }
}
