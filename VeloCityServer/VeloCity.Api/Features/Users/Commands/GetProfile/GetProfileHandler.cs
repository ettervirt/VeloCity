using MediatR;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Commands.GetProfile;

public class GetProfileHandler(
    ApplicationDbContext context) : IRequestHandler<GetProfileQuery, ProfileDto?>
{
    public async Task<ProfileDto?> Handle(GetProfileQuery request,
        CancellationToken ct)
    {
        var user = await context.Users.FindAsync([
                request.UserId
            ],
            ct);
        return user == null
            ? null
            : new ProfileDto(user.Name,
                user.Surname,
                user.Email,
                user.Role.ToString());
    }
}
