using MediatR;

namespace VeloCity.Api.Features.Lines.Commands.RemoveStop;

public record RemoveStopCommand(
    int LineId,
    int StopId,
    int Direction
) : IRequest<bool>;
