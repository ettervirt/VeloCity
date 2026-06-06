using MediatR;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Trips.Commands.UpdateTrip;

public record UpdateTripCommand(
    int Id,
    int LineId,
    int VehicleId,
    int DriverId,
    bool IsActive,
    DateTime Date,
    TripStatus Status
) : IRequest;

