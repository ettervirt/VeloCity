using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Features.Tickets.Commands.CreateTicketType;
using VeloCity.Api.Features.Tickets.Commands.DeleteTicketType;
using VeloCity.Api.Features.Tickets.Commands.UpdateTicketType;
using VeloCity.Api.Features.Tickets.Queries.GetActiveTicketTypes;
using VeloCity.Api.Features.Tickets.Queries.GetTicketType;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class TicketsController(
    IMediator mediator) : ControllerBase
{

    // get all tickets types
    [Authorize]
    [HttpGet("types")]
    [ProducesResponseType(typeof(List<TicketTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveTicketTypes()
    {
        var response = await mediator.Send(new GetActiveTicketTypesQuery());
        return Ok(response);
    }

    // get single tickets types
    [Authorize]
    [HttpGet("types/{id}")]
    [ProducesResponseType(typeof(TicketTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketType(int id)
    {
        var response = await mediator.Send(new GetTicketTypeQuery(id));
        return Ok(response);
    }

    // ADMIN ONLY: delete ticket type
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("types/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTicketType(int id)
    {
        await mediator.Send(new DeleteTicketTypeCommand(id));
        return NoContent();
    }

    // ADMIN ONLY: Create ticket type
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost("types")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTicketType([FromBody] CreateTicketTypeCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTicketType), new { id }, id);
    }

    // ADMIN ONLY: Update ticket type
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPut("types/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTicketType(int id, [FromBody] UpdateTicketTypeCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");

        await mediator.Send(command);
        return NoContent();
    }
}
