namespace VeloCity.Api.Features.Timetable.DTOs;

public record TimetableDto(
    int Id,
    int TripId,
    int StopId,
    string StopName,
    int Sequence,
    TimeSpan DepartureTime
);

