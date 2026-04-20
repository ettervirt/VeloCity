using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Commands.DeleteOwnAccount;

public class DeleteOwnAccountHandler(ApplicationDbContext context, IUserContext userContext)
    : IRequestHandler<DeleteOwnAccountCommand>
{
    public async Task Handle(DeleteOwnAccountCommand request, CancellationToken ct)
    {
        int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);

        var user = await context.Users.FindAsync([userId], ct);

        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }

        user.IsActive = false;

        await context.SaveChangesAsync(ct);
    }
}
