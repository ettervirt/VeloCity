using MediatR;
using VeloCity.Api.Features.Users.Queries.GetPayment;

namespace VeloCity.Api.Features.Users.Commands.TopUpBalance;

public record TopUpBalanceCommand(decimal Amount, string PaymentMethod) : IRequest<PaymentDto>;
