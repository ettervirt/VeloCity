using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Models;

public class Trip
{
    public int Id { get; set; }
    public int LineId { get; set; }
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public DateOnly Date { get; set; }
    public TripStatus Status { get; set; }
    public bool IsActive { get; set; }

    public virtual Line Line { get; set; } = null!;
    public virtual Vehicle Vehicle { get; set; } = null!;
    public virtual User Driver { get; set; } = null!;
    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
