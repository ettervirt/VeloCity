using MediatR;

namespace VeloCity.Api.Features.Trips.Commands.UpdateTrip;

public record UpdateTripCommand(
    int Id,
    int LineId,
    int VehicleId,
    int DriverId,
    bool IsActive,
    DateTime Date,
    string Status
) : IRequest;

