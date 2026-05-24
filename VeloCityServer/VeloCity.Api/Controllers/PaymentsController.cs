using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Payment.Commands.TopUpBalance;
using VeloCity.Api.Features.Payment.Queries.GetBalance;
using VeloCity.Api.Features.Payment.Queries.GetPayment;
using VeloCity.Api.Features.Payment.Queries.GetPaymentHistory;

namespace VeloCity.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PaymentsController(IMediator mediator) : ControllerBase
{
    //top-up
    [HttpPost("top-up")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TopUp([FromBody] TopUpBalanceCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    // get current balance
    [HttpGet("balance")]
    [ProducesResponseType(typeof(BalanceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBalance()
    {
        var result = await mediator.Send(new GetBalanceQuery());
        return Ok(result);
    }

    // get transaction
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPayment(int id)
    {
        var result = await mediator.Send(new GetPaymentQuery(id));
        return Ok(result);
    }

    // get transaction list
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<PaymentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentHistory([FromQuery] GetPaymentHistoryQuery query)
    {
        query.SortDirection = string.IsNullOrWhiteSpace(query.SortDirection) ? "desc" : query.SortDirection;
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
