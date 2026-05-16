using MediatR;
using VeloCity.Api.Common.DTOs;

namespace VeloCity.Api.Features.Vehicles.Queries.GetVehicles;

public record GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>;
