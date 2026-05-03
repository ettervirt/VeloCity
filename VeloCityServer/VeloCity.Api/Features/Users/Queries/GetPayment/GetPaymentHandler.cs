using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Features.Users.Queries.GetBalance;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Queries.GetPayment
{
    public class GetPaymentHandler(
    ApplicationDbContext context, IUserContext userContext) : IRequestHandler<GetPaymentQuery, PaymentDto?>
    {
        public async Task<PaymentDto?> Handle(GetPaymentQuery request, CancellationToken ct)
        {
            int userId = userContext.Id ?? throw new AppException("Missing user id", StatusCodes.Status401Unauthorized);
            var payment = await context.Payments
                .FirstOrDefaultAsync(payment => payment.Id == request.Id && payment.UserId == userId, ct);

            if(payment == null)
            {
                throw new NotFoundException("Payment", request.Id);
            }

            return new PaymentDto(payment.Amount, payment.Currency, payment.PaymentMethod, payment.TransactionId, payment.Status, payment.CreatedAt);
        }
    }
}
