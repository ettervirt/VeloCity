using MediatR;

namespace VeloCity.Api.Features.Stops.Commands.CreateStop;

public record CreateStopCommand(
    string Name,
    double Latitude,
    double Longitude,
    int Zone,
    int ExternalId) : IRequest<int>;
