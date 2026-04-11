using MediatR;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Commands.UpdateUserStatus;

public record UpdateUserStatusCommand(int UserId, UserRole Role, bool IsActive) : IRequest;
