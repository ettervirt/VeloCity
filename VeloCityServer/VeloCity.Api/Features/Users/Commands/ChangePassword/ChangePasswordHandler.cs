using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Commands.ChangePassword;

public class ChangePasswordHandler(ApplicationDbContext context, IUserContext userContext)
    : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);
        var user = await context.Users.FindAsync([userId], ct)
                   ?? throw new AppException("User don't exist",400);


        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new AppException("Old password don't match", 400);
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        await context.SaveChangesAsync(ct);
    }
}
