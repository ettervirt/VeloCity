using MediatR;

namespace VeloCity.Api.Features.Vehicles.Commands.DeleteVehicle;

public record DeleteVehicleCommand(int Id) : IRequest;
