using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Common.Services.Currency;
using VeloCity.Api.Features.Users.Queries.GetPayment;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Commands.TopUpBalance;

public class TopUpBalanceHandler(ApplicationDbContext context, IUserContext userContext, ICurrencyService currencyService)
    : IRequestHandler<TopUpBalanceCommand, PaymentDto>
{
    public async Task<PaymentDto> Handle(TopUpBalanceCommand request, CancellationToken ct)
    {
        int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);
        var user = await context.Users.FindAsync([userId], ct)
                   ?? throw new AppException("User doesn't exist",400);

        if(!Enum.TryParse<PaymentMethod>(request.PaymentMethod.ToString(), true, out var validatedMethod) 
            || !Enum.IsDefined(typeof(PaymentMethod), validatedMethod))
        {
            throw new AppException($"Invalid payment method: {request.PaymentMethod}", 400);
        }

        if (!Enum.TryParse<Currency>(request.Currency.ToString(), true, out var validatedCurrency)
            || !Enum.IsDefined(typeof(Currency), validatedCurrency))
        {
            throw new AppException($"Invalid currency: {request.Currency}", 400);
        }

        var rate = await currencyService.GetExchangeRateAsync(request.Currency.ToString(), ct);
        var amountInBaseCurrency = Math.Round(request.Amount * rate, 2);

        user.Balance += amountInBaseCurrency;

        var payment = new Payment
        {
            UserId = userId,
            Amount = request.Amount,
            ExchangeRate = rate,
            AmountInBaseCurrency = amountInBaseCurrency,
            Currency = validatedCurrency,
            PaymentMethod = validatedMethod,
            TransactionId = $"TNX-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpperInvariant()}",
            Status = PaymentStatus.Completed,
            CreatedAt = DateTime.UtcNow
        };
        context.Payments.Add(payment);

        await context.SaveChangesAsync(ct);

        return new PaymentDto(payment.Amount, payment.ExchangeRate, payment.AmountInBaseCurrency, payment.Currency, payment.PaymentMethod, payment.TransactionId, payment.Status, payment.CreatedAt);
    }
}
