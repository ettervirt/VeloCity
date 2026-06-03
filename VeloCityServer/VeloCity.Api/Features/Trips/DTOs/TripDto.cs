namespace VeloCity.Api.Features.Trips.DTOs;

public record TripDto(
    int Id,
    int LineId,
    string LineName,
    int VehicleId,
    int DriverId,
    bool IsActive,
    DateOnly Date,
    string Status
);
