using MediatR;

namespace VeloCity.Api.Features.Payment.Queries.GetBalance;

public record GetBalanceQuery : IRequest<BalanceDto>;

public record BalanceDto(
    decimal Balance);
