using FluentValidation;
using MediatR;

namespace VeloCity.Api.Features.Users.Commands.ChangePassword;

public record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword) : IRequest;
