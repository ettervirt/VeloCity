using MediatR;
using VeloCity.Api.Common.DTOs;

namespace VeloCity.Api.Features.Vehicles.Commands.CreateVehicle;

public record CreateVehicleCommand(string SideNumber, string Model) : IRequest<VehicleDto>;
