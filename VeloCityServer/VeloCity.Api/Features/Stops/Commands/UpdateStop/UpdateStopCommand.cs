using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Stops.Commands.UpdateStop;

public record UpdateStopCommand(
    int Id,
    string Name,
    double Latitude,
    double Longitude,
    int Zone,
    int ExternalId) : IRequest;
