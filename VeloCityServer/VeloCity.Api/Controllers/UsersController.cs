using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Common.DTOs;
using VeloCity.Api.Features.Users.Commands.ChangePassword;
using VeloCity.Api.Features.Users.Commands.Login;
using VeloCity.Api.Features.Users.Commands.CreateUser;
using VeloCity.Api.Features.Users.Commands.DeleteOwnAccount;
using VeloCity.Api.Features.Users.Commands.DeleteUser;
using VeloCity.Api.Features.Users.Commands.TopUpBalance;
using VeloCity.Api.Features.Users.Commands.UpdateProfile;
using VeloCity.Api.Features.Users.Queries.GetBalance;
using VeloCity.Api.Features.Users.Queries.GetProfile;

namespace VeloCity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    IMediator mediator) : ControllerBase {

    // login
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        LoginResponse? response = await mediator.Send(command);

        if (response == null)
            return Unauthorized(new
            {
                message = "Wrong username or password"
            });

        return Ok(response);
    }

    // register
    [HttpPost("register")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var userId = await mediator.Send(command);

        return CreatedAtAction(
            nameof(GetProfile),
            new { id = userId },
            new { id = userId, message = "User added successfully" }
        );
    }

    // get user profile
    [Authorize]
    [HttpGet("profile")]
    [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProfile()
    {
        var profile = await mediator.Send(new GetProfileQuery());
        return Ok(profile);
    }

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
    [ProducesResponseType(typeof(BalanceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TopUp([FromBody] TopUpBalanceCommand command)
    {
        var newBalance = await mediator.Send(command);
        return Ok(newBalance);
    }

    // self delete user
    [Authorize]
    [HttpDelete("me")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMe()
    {
        await mediator.Send(new DeleteOwnAccountCommand());
        return NoContent();
    }

    // update my user
    [Authorize]
    [HttpPut("me")]
    [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
    {
        var updatedProfile = await mediator.Send(command);
        return Ok(updatedProfile);
    }

    // update my password
    [Authorize]
    [HttpPut("me/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    // ADMIN ONLY: delete user
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }
}
