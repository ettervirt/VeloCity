using MediatR;

namespace VeloCity.Api.Features.Vehicles.Commands.UpdateVehicle;
public record UpdateVehicleRequest(int Id, UpdateVehicleCommand Command) : IRequest<bool>;
