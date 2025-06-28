using TheBoys.Application.Features.Users.Commands.Login;

namespace TheBoys.API.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("auth")]
    public async Task<ActionResult<ResponseOf<LoginUserResult>>> LoginAsync(
        [FromBody] LoginUserCommand command,
        CancellationToken cancellationToken
    ) => await mediator.Send(command, cancellationToken);
}
