using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Payment.Queries.GetBalance;

public class GetBalanceHandler(
    ApplicationDbContext context,
    IUserContext userContext) : IRequestHandler<GetBalanceQuery, BalanceDto>
{
    public async Task<BalanceDto> Handle(GetBalanceQuery request,
        CancellationToken ct)
    {
        var userId = userContext.Id ??
                     throw new AppException("Missing user ID.",
                         401);

        var user = await context.Users.FindAsync([
                           userId
                       ],
                       ct) ??
                   throw new NotFoundException("User",
                       userId);

        return new BalanceDto(user.Balance);
    }
}
