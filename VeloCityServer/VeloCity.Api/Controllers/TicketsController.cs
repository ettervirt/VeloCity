using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Features.Tickets.Queries.GetActiveTicketTypes;

namespace VeloCity.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class TicketsController(
    IMediator mediator) : ControllerBase {

    // tickets types
    [Authorize]
    [HttpGet("types")]
    [ProducesResponseType(typeof(List<TicketTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveTicketTypes()
    {
        var response = await mediator.Send(new GetActiveTicketTypesQuery());
        return Ok(response);
    }
}
