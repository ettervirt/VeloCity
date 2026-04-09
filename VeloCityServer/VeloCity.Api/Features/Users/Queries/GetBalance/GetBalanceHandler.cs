using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Queries.GetBalance;

public class GetBalanceHandler(
    ApplicationDbContext context, IUserContext userContext) : IRequestHandler<GetBalanceQuery, BalanceDto?>
{
    public async Task<BalanceDto?> Handle(GetBalanceQuery request, CancellationToken ct)
    {
        int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);
        var user = await context.Users.FindAsync([userId], ct);

        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }

        return  new BalanceDto(user.Balance);
    }
}
