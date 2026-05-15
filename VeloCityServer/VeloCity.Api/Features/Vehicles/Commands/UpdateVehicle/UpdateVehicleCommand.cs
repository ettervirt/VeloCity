using MediatR;

namespace VeloCity.Api.Features.Vehicles.Commands.UpdateVehicle;

public record UpdateVehicleCommand(string SideNumber, string Model) : IRequest<bool>;

