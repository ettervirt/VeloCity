using MediatR;
using VeloCity.Api.Features.Vehicles.DTOs;

namespace VeloCity.Api.Features.Vehicles.Queries.GetVehicleById;

public record GetVehicleByIdQuery(int Id) : IRequest<VehicleDto>;
