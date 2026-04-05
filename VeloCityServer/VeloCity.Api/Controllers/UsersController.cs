using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Features.Users.Commands.GetProfile;
using VeloCity.Api.Features.Users.Commands.Login;
using VeloCity.Api.Features.Users.Commands.CreateUser;

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
        var response = await mediator.Send(command);

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
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var profile = await mediator.Send(new GetProfileQuery(userId));
        return Ok(profile);
    }
}
