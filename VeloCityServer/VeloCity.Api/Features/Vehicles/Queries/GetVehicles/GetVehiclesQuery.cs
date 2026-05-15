using MediatR;
using VeloCity.Api.Features.Vehicles.Queries.GetVehicle;

namespace VeloCity.Api.Features.Vehicles.Queries.GetVehicles;

public record GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>;
