using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Payment.Queries.GetPayment;

public class GetPaymentHandler(
    ApplicationDbContext context,
    IUserContext userContext) : IRequestHandler<GetPaymentQuery, PaymentDto>
{
    public async Task<PaymentDto> Handle(GetPaymentQuery request,
        CancellationToken ct)
    {
        var userId = userContext.Id ??
                     throw new AppException("Missing user ID.",
                         401);
        var payment = await context.Payments
                          .AsNoTracking()
                          .FirstOrDefaultAsync(p => p.Id == request.Id && p.UserId == userId,
                              ct) ??
                      throw new NotFoundException("Payment",
                          request.Id);

        return new PaymentDto(
            payment.Id,
            payment.Amount,
            payment.ExchangeRate,
            payment.AmountInBaseCurrency,
            payment.Currency,
            payment.PaymentMethod,
            payment.TransactionId,
            payment.Status,
            payment.CreatedAt);
    }
}
