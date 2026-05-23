using MediatR;
using VeloCity.Api.Features.Payment.Queries.GetPayment;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Payment.Commands.TopUpBalance;

public record TopUpBalanceCommand(
    decimal Amount,
    PaymentMethod PaymentMethod,
    Currency Currency) : IRequest<PaymentDto>;
