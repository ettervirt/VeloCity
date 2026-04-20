namespace VeloCity.Api.Models;

public class TicketType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationInMinutes { get; set; }
    public bool IsActive { get; set; } = true;
    public int ZoneLimit { get; set; }
}
