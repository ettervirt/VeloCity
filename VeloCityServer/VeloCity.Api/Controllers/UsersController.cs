using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Features.Users.Commands.GetProfile;
using VeloCity.Api.Features.Users.Commands.Login;
using VeloCity.Api.Features.Users.Commands.CreateUser;
using VeloCity.Api.Features.Users.Commands.GetBalance;

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
    public async Task<IActionResult> GetProfile()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        ProfileDto? profile = await mediator.Send(new GetProfileQuery(userId));
        if (profile == null)
            return Unauthorized(new
            {
                message = "User don't exist"
            });
        return Ok(profile);
    }

    // get user Balance
    [Authorize]
    [HttpGet("balance")]
    [ProducesResponseType(typeof(BalanceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBalance()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        BalanceDto? balance = await mediator.Send(new GetBalanceQuery(userId));
        if (balance == null)
            return Unauthorized(new
            {
                message = "User don't exist"
            });
        return Ok(balance);
    }
}
