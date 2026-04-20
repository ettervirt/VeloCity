using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Commands.UpdateUserStatus;

public record UpdateStatusRequest(UserRole Role, bool IsActive);
