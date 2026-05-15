using MediatR;

namespace VeloCity.Api.Features.Vehicles.Queries.GetVehicle
{
    public record GetVehicleByIdQuery(int Id) : IRequest<VehicleDto?>;

    public record VehicleDto(int Id, string SideNumber, string Model, bool IsActive);
}
