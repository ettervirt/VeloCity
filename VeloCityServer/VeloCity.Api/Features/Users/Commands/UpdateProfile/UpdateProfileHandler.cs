using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Features.Users.Queries.GetProfile;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Commands.UpdateProfile;

public class UpdateProfileHandler(ApplicationDbContext context, IUserContext userContext)
    : IRequestHandler<UpdateProfileCommand, ProfileDto>
{
    public async Task<ProfileDto> Handle(UpdateProfileCommand request, CancellationToken ct)
    {
        int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);
        var user = await context.Users.FindAsync([userId], ct)
                   ?? throw new AppException("User don't exist", 400);

        user.Name = request.Name;
        user.Surname = request.Surname;

        await context.SaveChangesAsync(ct);

        return new ProfileDto(user.Name, user.Surname, user.Email, user.Role);
    }
}
