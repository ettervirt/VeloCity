using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Features.Tickets.Commands.DeleteTicketType;
using VeloCity.Api.Features.Tickets.Queries.GetActiveTicketTypes;
using VeloCity.Api.Models.Enums;

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

    // ADMIN ONLY: delete ticket type
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTicketType(int id)
    {
        await mediator.Send(new DeleteTicketTypeCommand(id));
        return NoContent();
    }
}
