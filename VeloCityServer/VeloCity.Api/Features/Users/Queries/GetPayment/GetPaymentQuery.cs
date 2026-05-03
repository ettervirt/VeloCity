using MediatR;
using VeloCity.Api.Features.Users.Queries.GetBalance;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Queries.GetPayment
{
    public record GetPaymentQuery(int Id) : IRequest<PaymentDto?>;

    public record PaymentDto(decimal Amount, Currency Currency, PaymentMethod paymentMethod, string TransactionId, PaymentStatus status, DateTime createdAt);
}
