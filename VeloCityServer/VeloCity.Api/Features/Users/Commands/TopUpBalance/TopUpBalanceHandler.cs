using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Features.Users.Queries.GetBalance;
using VeloCity.Api.Features.Users.Queries.GetPayment;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Commands.TopUpBalance;

public class TopUpBalanceHandler(ApplicationDbContext context, IUserContext userContext)
    : IRequestHandler<TopUpBalanceCommand, PaymentDto>
{
    public async Task<PaymentDto> Handle(TopUpBalanceCommand request, CancellationToken ct)
    {
        int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);
        var user = await context.Users.FindAsync([userId], ct)
                   ?? throw new AppException("User don't exist",400);

        user.Balance += request.Amount;

        if(!Enum.TryParse<PaymentMethod>(request.PaymentMethod, true, out var validatedMethod))
        {
            throw new AppException("Invalid payment method", 400);
        }

        var payment = new Payment
        {
            UserId = userId,
            Amount = request.Amount,
            Currency = Currency.PLN,
            PaymentMethod = validatedMethod,
            TransactionId = $"TNX-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            Status = PaymentStatus.Completed,
            CreatedAt = DateTime.UtcNow
        };
        context.Payments.Add(payment);

        await context.SaveChangesAsync(ct);

        return new PaymentDto(payment.Amount, payment.Currency, payment.PaymentMethod, payment.TransactionId, payment.Status, payment.CreatedAt);
    }
}
