using MediatR;

namespace VeloCity.Api.Features.Lines.Commands.AddStop;

public record AddStopCommand(
    int LineId,
    int StopId,
    int Direction
) : IRequest;

public record AddStopBody(
    int StopId, 
    int Direction);
