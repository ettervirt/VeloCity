using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Features.Users.Commands.Login;

namespace VeloCity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    IMediator mediator) : ControllerBase {
    [HttpPost("login")]
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
}
