using MediatR;

namespace VeloCity.Api.Features.Trips.Commands.CreateTrip;

public record CreateTripCommand(
    int LineId,
    int VehicleId,
    int DriverId,
    DateOnly Date,
    TimeSpan StartTime
): IRequest<int>;
