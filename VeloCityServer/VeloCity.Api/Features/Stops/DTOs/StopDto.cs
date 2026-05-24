namespace VeloCity.Api.Features.Stops.DTOs;

public record StopDto(
    int Id,
    string Name,
    double Latitude,
    double Longitude,
    int Zone,
    int? ExternalId,
    bool IsActive);
