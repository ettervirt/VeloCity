namespace VeloCity.Api.Models;

public class RouteStop {
    public int Id { get; set; }
    public int LineId { get; set; }
    public virtual Line Line { get; set; } = null!;
    public int StopId { get; set; }
    public virtual Stop Stop { get; set; } = null!;
    public int Sequence { get; set; }
    public int Direction { get; set; }
}
