using MediatR;
using VeloCity.Api.Common.DTOs;

namespace VeloCity.Api.Features.Vehicles.Queries.GetVehicleById;

public record GetVehicleByIdQuery(int Id) : IRequest<VehicleDto?>;
