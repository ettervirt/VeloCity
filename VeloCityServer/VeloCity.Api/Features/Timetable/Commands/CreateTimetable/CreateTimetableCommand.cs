using MediatR;

namespace VeloCity.Api.Features.Timetable.Commands.CreateTimetable;

public record CreateTimetableCommand(
    int TripId,
    int StopId,
    int Sequence,
    TimeSpan DepartureTime
) : IRequest<int>;
