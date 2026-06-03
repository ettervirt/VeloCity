using MediatR;

namespace VeloCity.Api.Features.Timetable.Commands.UpdateTimetable
{
    public record UpdateTimetableCommand(
        int Id,
        int TripId,
        int StopId,
        int Sequence,
        TimeSpan DepartureTime
    ) : IRequest;
}
