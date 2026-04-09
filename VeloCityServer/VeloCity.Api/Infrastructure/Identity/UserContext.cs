using System.Security.Claims;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Infrastructure.Identity;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
    public int? Id => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
    public string? Email => User?.FindFirstValue(ClaimTypes.Email);
    public string? Role => User?.FindFirstValue(ClaimTypes.Role);
    public bool IsAdmin => Role == nameof(UserRole.Admin);
    public bool IsDriver => Role == nameof(UserRole.Driver);
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
}
