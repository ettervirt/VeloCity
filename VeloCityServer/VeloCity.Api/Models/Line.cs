namespace VeloCity.Api.Models;

public class Line
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
}
