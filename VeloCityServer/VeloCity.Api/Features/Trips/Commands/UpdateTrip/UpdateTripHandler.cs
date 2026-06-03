using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Trips.Commands.UpdateTrip;

public class UpdateTripHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateTripCommand>
{
    public async Task Handle(UpdateTripCommand request, CancellationToken ct)
    {
        var trip = await context.Trips
            .FirstOrDefaultAsync(t => t.Id == request.Id, ct)
            ?? throw new NotFoundException("Trip", request.Id);
        trip.LineId = request.LineId;
        trip.VehicleId = request.VehicleId;
        trip.DriverId = request.DriverId;
        trip.IsActive = request.IsActive;
        trip.Date = DateOnly.FromDateTime(request.Date);
        if (Enum.TryParse<TripStatus>(request.Status, true, out var parsedStatus))
        {
            trip.Status = parsedStatus;
        }
        else
        {
            trip.Status = TripStatus.Scheduled;
        }

        await context.SaveChangesAsync(ct);
    }
}
