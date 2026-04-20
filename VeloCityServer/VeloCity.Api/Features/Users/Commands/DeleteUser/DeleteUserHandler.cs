using MediatR;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Common.Exceptions;

namespace VeloCity.Api.Features.Users.Commands.DeleteUser;

public class DeleteUserHandler(ApplicationDbContext context, IUserContext userContext)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken ct)
    {
        if (request.UserId == userContext.Id)
        {
            throw new AppException("Cannot delete own account", 400);
        }

        var user = await context.Users.FindAsync([request.UserId], ct)
                   ?? throw new NotFoundException("User", request.UserId);

        if (!user.IsActive)
        {
            throw new AppException("User already inactive", 400);
        }

        user.IsActive = false;

        await context.SaveChangesAsync(ct);
    }
}
