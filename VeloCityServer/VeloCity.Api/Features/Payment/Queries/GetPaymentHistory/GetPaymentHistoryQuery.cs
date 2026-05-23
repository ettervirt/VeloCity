using MediatR;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Payment.Queries.GetPayment;

namespace VeloCity.Api.Features.Payment.Queries.GetPaymentHistory;

public class GetPaymentHistoryQuery : PaginatedRequest,
    IRequest<PaginatedList<PaymentDto>>
{ }
