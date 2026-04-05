using MediatR;

namespace VeloCity.Api.Features.Users.Commands.GetBalance;

public record GetBalanceQuery(int UserId) : IRequest<BalanceDto?>;

public record BalanceDto(Decimal Balance);
