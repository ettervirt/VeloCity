using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Common.Services.Currency;
using VeloCity.Api.Features.Payment.Queries.GetPayment;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Payment.Commands.TopUpBalance;

public class TopUpBalanceHandler(
    ApplicationDbContext context,
    IUserContext userContext,
    ICurrencyService currencyService)
    : IRequestHandler<TopUpBalanceCommand, PaymentDto>
{
    public async Task<PaymentDto> Handle(TopUpBalanceCommand request,
        CancellationToken ct)
    {
        var userId = userContext.Id ??
                     throw new AppException("Missing user ID.",
                         401);
        var user = await context.Users.FindAsync([
                           userId
                       ],
                       ct) ??
                   throw new NotFoundException("User",
                       userId);

        var rate = await currencyService.GetExchangeRateAsync(request.Currency.ToString(),
            ct);
        var amountInBaseCurrency = Math.Round(request.Amount * rate,
            2);

        user.Balance += amountInBaseCurrency;

        var payment = new Models.Payment
        {
            UserId = userId,
            Amount = request.Amount,
            ExchangeRate = rate,
            AmountInBaseCurrency = amountInBaseCurrency,
            Currency = request.Currency,
            PaymentMethod = request.PaymentMethod,
            TransactionId = $"TNX-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpperInvariant()}",
            Status = PaymentStatus.Completed,
            CreatedAt = DateTime.UtcNow
        };

        context.Payments.Add(payment);
        await context.SaveChangesAsync(ct);

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
