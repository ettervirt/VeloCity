using MediatR;

namespace VeloCity.Api.Features.Lines.Commands.UpdateSequence;

public record UpdateSequenceCommand(
    int LineId,
    int Direction,
    List<int> NewStopIds
) : IRequest<bool>;

public record UpdateSequenceBody(
    int Direction, 
    List<int> NewStopIds);
