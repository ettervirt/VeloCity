using MediatR;
using VeloCity.Api.Features.Users.Queries.GetBalance;

namespace VeloCity.Api.Features.Users.Commands.TopUpBalance;

public record TopUpBalanceCommand(decimal Amount) : IRequest<BalanceDto>;
