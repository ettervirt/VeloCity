using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Queries.GetProfile;

public class GetProfileHandler(ApplicationDbContext context, IUserContext userContext)
    : IRequestHandler<GetProfileQuery, ProfileDto?>
{
    public async Task<ProfileDto?> Handle(GetProfileQuery request, CancellationToken ct)
    {
        int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);
        var user = await context.Users.FindAsync([userId], ct);

        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }

        return new ProfileDto(user.Name, user.Surname, user.Email, user.Role);
    }
}
