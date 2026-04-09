using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Features.Users.Queries.GetBalance;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Commands.TopUpBalance;

public class TopUpBalanceHandler(ApplicationDbContext context, IUserContext userContext)
    : IRequestHandler<TopUpBalanceCommand, BalanceDto>
{
    public async Task<BalanceDto> Handle(TopUpBalanceCommand request, CancellationToken ct)
    {
        int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);
        var user = await context.Users.FindAsync([userId], ct)
                   ?? throw new AppException("User don't exist",400);

        user.Balance += request.Amount;
        await context.SaveChangesAsync(ct);

        return new BalanceDto(user.Balance);
    }
}
