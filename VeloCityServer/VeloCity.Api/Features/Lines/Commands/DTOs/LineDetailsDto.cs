namespace VeloCity.Api.Features.Lines.Commands.DTOs;

public record LineDetailsDto(
    int Id,
    string Name,
    List<LineRouteStopDto> Stops
);

public record LineRouteStopDto(
    int StopId,
    string StopName,
    int Sequence,
    int Direction
);
