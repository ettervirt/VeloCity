using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Timetable.Commands.CreateTimetable;
using VeloCity.Api.Features.Timetable.Commands.DeleteTimetable;
using VeloCity.Api.Features.Timetable.Commands.UpdateTimetable;
using VeloCity.Api.Features.Timetable.DTOs;
using VeloCity.Api.Features.Timetable.Queries.GetTimetable;
using VeloCity.Api.Features.Timetable.Queries.GetTimetables;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class TimetableController(IMediator mediator) : ControllerBase
{
    //admin only get timetables paginated and filterable
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<TimetableDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTimetables([FromQuery] GetTimetablesQuery query, CancellationToken ct)
    {
        var result = await mediator.Send(query, ct);
        return Ok(result);
    }

    //admin only get timetable by id
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TimetableDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTimetable(int id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTimetableQuery(id), ct);
        return Ok(result);
    }

    //admin only create timetable
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTimetable([FromBody] CreateTimetableCommand command, CancellationToken ct)
    {
        var id = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetTimetable), new { id }, id);
    }

    //admin only update timetable
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTimetable(int id, [FromBody] UpdateTimetableCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");

        await mediator.Send(command, ct);
        return NoContent();
    }

    //admin only delete timetable
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTimetable(int id, CancellationToken ct)
    {
        await mediator.Send(new DeleteTimetableCommand(id), ct);
        return NoContent();
    }
}
