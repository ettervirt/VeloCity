using MediatR;
using VeloCity.Api.Features.Users.Queries.GetPayment;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Commands.TopUpBalance;

public record TopUpBalanceCommand(decimal Amount, PaymentMethod PaymentMethod) : IRequest<PaymentDto>;
