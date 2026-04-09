namespace VeloCity.Api.Common.Interfaces;

public interface IUserContext
{
    int? Id { get; }
    string? Email { get; }
    string? Role { get; }
    bool IsAdmin { get; }
    bool IsDriver { get; }
    bool IsAuthenticated { get; }
}
