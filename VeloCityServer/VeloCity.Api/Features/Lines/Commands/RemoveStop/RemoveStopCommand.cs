using MediatR;

namespace VeloCity.Api.Features.Lines.Commands.RemoveStop;

public record RemoveStopCommand(
    int RouteStopId
) : IRequest;
