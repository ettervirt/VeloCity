namespace VeloCity.Api.Models;

public class Vehicle {
    public int Id { get; set; }
    public string SideNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
