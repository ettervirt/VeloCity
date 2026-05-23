using MediatR;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Payment.Queries.GetPayment;

public record GetPaymentQuery(
    int Id) : IRequest<PaymentDto>;

public record PaymentDto(
    int Id,
    decimal Amount,
    decimal ExchangeRate,
    decimal AmountInBaseCurrency,
    Currency Currency,
    PaymentMethod PaymentMethod,
    string TransactionId,
    PaymentStatus Status,
    DateTime CreatedAt);
