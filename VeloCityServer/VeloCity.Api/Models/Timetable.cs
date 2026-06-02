namespace VeloCity.Api.Models;

public class Timetable
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public int StopId { get; set; }
    public TimeSpan DepartureTime { get; set; }

    public virtual Trip Trip { get; set; } = null!;
    public virtual Stop Stop { get; set; } = null!;
}
