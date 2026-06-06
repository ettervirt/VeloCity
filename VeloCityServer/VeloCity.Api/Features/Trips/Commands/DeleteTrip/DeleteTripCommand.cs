using MediatR;

namespace VeloCity.Api.Features.Trips.Commands.DeleteTrip;

public record DeleteTripCommand(int Id) : IRequest;
