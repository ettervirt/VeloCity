using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Queries.GetUserDetails;

public class GetUserDetailsHandler(ApplicationDbContext context)
    : IRequestHandler<GetUserDetailsQuery, UserDetailsDto>
{
    public async Task<UserDetailsDto> Handle(GetUserDetailsQuery request, CancellationToken ct)
    {
        var user = await context.Users
                       .AsNoTracking()
                       .FirstOrDefaultAsync(u => u.Id == request.UserId, ct)
                   ?? throw new NotFoundException("User", request.UserId);

        return new UserDetailsDto(
            user.Id,
            user.Email,
            user.Name,
            user.Surname,
            user.Balance,
            user.Role,
            user.IsActive
        );
    }
}
