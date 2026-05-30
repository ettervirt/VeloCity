using MediatR;

namespace VeloCity.Api.Features.Vehicles.Commands.UpdateVehicle;

public record UpdateVehicleCommand(int Id, string SideNumber, string Model) : IRequest;
