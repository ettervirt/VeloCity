using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Common.Interfaces;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Payment.Queries.GetPayment;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Payment.Queries.GetPaymentHistory;

public class GetPaymentHistoryHandler(
    ApplicationDbContext context,
    IUserContext userContext) : IRequestHandler<GetPaymentHistoryQuery, PaginatedList<PaymentDto>>
{
    public async Task<PaginatedList<PaymentDto>> Handle(GetPaymentHistoryQuery request, CancellationToken ct)
    {
        var userId = userContext.Id ?? throw new AppException("Missing user ID.", 401);

        var query = context.Payments
            .AsNoTracking()
            .Where(p => p.UserId == userId);

        query = request.IsDescending
            ? query.OrderByDescending(p => p.CreatedAt)
            : query.OrderBy(p => p.CreatedAt);

        var dtoQuery = query.Select(p => new PaymentDto(
            p.Id,
            p.Amount,
            p.ExchangeRate,
            p.AmountInBaseCurrency,
            p.Currency,
            p.PaymentMethod,
            p.TransactionId,
            p.Status,
            p.CreatedAt
        ));

        return await PaginatedList<PaymentDto>.CreateAsync(
            dtoQuery,
            request.PageNumber,
            request.PageSize,
            ct);
    }
}
