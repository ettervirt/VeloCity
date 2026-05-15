using MediatR;
using VeloCity.Api.Features.Vehicles.Queries.GetVehicle;

namespace VeloCity.Api.Features.Vehicles.Commands.CreateVehicle;

public record CreateVehicleCommand(string SideNumber, string Model) : IRequest<VehicleDto>;
