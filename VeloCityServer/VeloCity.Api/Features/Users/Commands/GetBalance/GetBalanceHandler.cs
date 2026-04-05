using MediatR;
using VeloCity.Api.Models.Data;


namespace VeloCity.Api.Features.Users.Commands.GetBalance;

public class GetBalanceHandler(
    ApplicationDbContext context) : IRequestHandler<GetBalanceQuery, BalanceDto?>
{
    public async Task<BalanceDto?> Handle(GetBalanceQuery request, CancellationToken ct)
    {
            var user = await context.Users.FindAsync([
                    request.UserId], ct);
            return  user == null
                ? null
                : new BalanceDto(user.Balance);
    }
}
