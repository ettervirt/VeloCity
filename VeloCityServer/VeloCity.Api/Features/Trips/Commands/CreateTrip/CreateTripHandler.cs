using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Trips.Commands.CreateTrip
{
    public class CreateTripHandler(ApplicationDbContext context)
        :IRequestHandler<CreateTripCommand, int>
    {
        public async Task<int> Handle(CreateTripCommand request, CancellationToken ct)
        {
            var lineExists = await context.Lines.AnyAsync(l => l.Id == request.LineId, ct);
            if (!lineExists) throw new NotFoundException("Line", request.LineId);

            var trip = new Trip
            {
                LineId = request.LineId,
                VehicleId = request.VehicleId,
                DriverId = request.DriverId,
                Date = request.Date,
                Status = TripStatus.Scheduled,
                IsActive = true
            };

            context.Trips.Add(trip);
            await context.SaveChangesAsync(ct);

            var routeStops = await context.RouteStops
                .Where(rs => rs.LineId == request.LineId)
                .OrderBy(rs => rs.Sequence)
                .ToListAsync(ct);

            var currentDepartureTime = request.StartTime;
            var timetables = new List<VeloCity.Api.Models.Timetable>();

            foreach (var routeStop in routeStops)
            {
                var timetableEntry = new VeloCity.Api.Models.Timetable
                {
                    TripId = trip.Id,
                    StopId = routeStop.StopId,
                    Sequence = routeStop.Sequence,
                    DepartureTime = currentDepartureTime
                };
                timetables.Add(timetableEntry);
                currentDepartureTime = currentDepartureTime.Add(TimeSpan.FromMinutes(3));
            }
            context.Timetables.AddRange(timetables);
            await context.SaveChangesAsync(ct);
            return trip.Id;
        }
    }
}
