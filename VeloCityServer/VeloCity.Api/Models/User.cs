namespace VeloCity.Api.Models;
using VeloCity.Api.Models.Enums;

public class User {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0.00m;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
