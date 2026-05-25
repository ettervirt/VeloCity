using MediatR;
using VeloCity.Api.Features.Vehicles.DTOs;

namespace VeloCity.Api.Features.Vehicles.Queries.GetVehicles;

public record GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>;
