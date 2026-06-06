using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Features.Trips.Commands.CreateTrip;
using VeloCity.Api.Features.Trips.Commands.DeleteTrip;
using VeloCity.Api.Features.Trips.Commands.UpdateTrip;
using VeloCity.Api.Features.Trips.DTOs;
using VeloCity.Api.Features.Trips.Queries.GetTrips;
using VeloCity.Api.Features.Trips.Queries.GetTrip;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TripsController(IMediator mediator) : ControllerBase
{
    //ADMIN ONLY: create trip
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateTrip([FromBody] CreateTripCommand command, CancellationToken ct)
    {
        var tripId = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetTrip), new { id = tripId }, new { Id = tripId });
    }

    //ADMIN ONLY: get trip by id
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTrip(int id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTripQuery(id), ct);
        return Ok(result);
    }

    //ADMIN ONLY: get all trips
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] GetTripsQuery query, CancellationToken ct)
    {
        var result = await mediator.Send(query, ct);
        return Ok(result);
    }

    //ADMIN ONLY: update trip
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTrip(int id, [FromBody] UpdateTripCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");

        await mediator.Send(command, ct);
        return NoContent();
    }

    //ADMIN ONLY: delete trip (soft delete)
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrip(int id, CancellationToken ct)
    {
        await mediator.Send(new DeleteTripCommand(id), ct);
        return NoContent();
    }
}
