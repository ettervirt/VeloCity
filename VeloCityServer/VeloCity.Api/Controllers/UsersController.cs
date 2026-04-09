using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Common.DTOs;
using VeloCity.Api.Features.Users.Commands.Login;
using VeloCity.Api.Features.Users.Commands.CreateUser;
using VeloCity.Api.Features.Users.Commands.TopUpBalance;
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
}
