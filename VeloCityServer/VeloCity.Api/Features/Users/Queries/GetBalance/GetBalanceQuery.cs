using MediatR;

namespace VeloCity.Api.Features.Users.Queries.GetBalance;

public record GetBalanceQuery() : IRequest<BalanceDto?>;

public record BalanceDto(Decimal Balance);
