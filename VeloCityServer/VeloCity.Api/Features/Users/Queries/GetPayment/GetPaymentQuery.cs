using MediatR;
using VeloCity.Api.Features.Users.Queries.GetBalance;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Queries.GetPayment;

public record GetPaymentQuery(int Id) : IRequest<PaymentDto?>;

public record PaymentDto(decimal Amount, decimal ExchangeRate, decimal AmountInBaseCurrency, Currency Currency, PaymentMethod PaymentMethod, string TransactionId, PaymentStatus Status, DateTime CreatedAt);
