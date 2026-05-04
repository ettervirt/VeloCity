using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Common.DTOs;
using VeloCity.Api.Features.Users.Commands.TopUpBalance;
using VeloCity.Api.Features.Users.Queries.GetBalance;
using VeloCity.Api.Features.Users.Queries.GetPayment;

namespace VeloCity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentsController(
IMediator mediator) : ControllerBase
{
    // get user Balance
    [Authorize]
    [HttpGet("balance")]
    [ProducesResponseType(typeof(BalanceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBalance()
    {
        BalanceDto? balance = await mediator.Send(new GetBalanceQuery());
        return Ok(balance);
    }

    // topup user Balance
    [HttpPost("balance/topup")]
    [Authorize]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TopUp([FromBody] TopUpBalanceCommand command)
    {
        var payment = await mediator.Send(command);
        return Ok(payment);
    }
}
