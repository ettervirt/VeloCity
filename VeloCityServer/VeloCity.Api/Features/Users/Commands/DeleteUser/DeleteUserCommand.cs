using MediatR;

namespace VeloCity.Api.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(int UserId) : IRequest;
