namespace VeloCity.Api.Features.Vehicles.DTOs;

public record VehicleDto(
    int Id, 
    string SideNumber, 
    string Model, 
    bool IsActive);
