using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Commands.UpdateUserStatus;

public class UpdateUserStatusHandler(ApplicationDbContext context, IUserContext userContext)
    : IRequestHandler<UpdateUserStatusCommand>
{
    public async Task Handle(UpdateUserStatusCommand request, CancellationToken ct)
    {

        if (request.UserId == userContext.Id && request.Role != UserRole.Admin)
        {
            throw new AppException("You cannot remove own admin privileges", 403);
        }

        if (request.UserId == userContext.Id && !request.IsActive)
        {
            throw new AppException("You cannot Block yourself", 403);
        }

        var user = await context.Users.FindAsync([request.UserId], ct)
                   ?? throw new NotFoundException("User", request.UserId);

        user.Role = request.Role;
        user.IsActive = request.IsActive;

        await context.SaveChangesAsync(ct);
    }
}
