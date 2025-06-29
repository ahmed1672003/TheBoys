using Microsoft.AspNetCore.Authorization;
using TheBoys.Application.Features.Users.Commands.AddUser;
using TheBoys.Application.Features.Users.Commands.ChangePassword;
using TheBoys.Application.Features.Users.Commands.Delete;
using TheBoys.Application.Features.Users.Commands.Login;
using TheBoys.Application.Features.Users.Commands.Update;
using TheBoys.Application.Features.Users.Queries.GetById;
using TheBoys.Application.Features.Users.Queries.GetProfile;

namespace TheBoys.API.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///  login user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseOf<AuthUserResult>>> AuthAsync(
        [FromBody] AuthUserCommand command,
        CancellationToken cancellationToken
    ) => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// add user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost()]
    [Authorize()]
    public async Task<ActionResult<Response>> AddUserAsync(
        [FromBody] AddUserCommand command,
        CancellationToken cancellationToken = default
    ) => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// change password
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("ch-pa")]
    [Authorize()]
    public async Task<ActionResult<Response>> ChangePasswordAsync(
        [FromBody] ChangePasswordCommand command,
        CancellationToken cancellationToken = default
    ) => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// update user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut()]
    [Authorize()]
    public async Task<ActionResult<Response>> UpdateAsync(
        [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken = default
    ) => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// get user by id | used from super admin
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize()]
    public async Task<ActionResult<Response>> GetUserByIdAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    ) => await mediator.Send(new GetUserByIdQuery(id), cancellationToken);

    /// <summary>
    /// get user profile | used from via current user to get his profile
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet()]
    [Authorize()]
    public async Task<ActionResult<Response>> GetUserProfileAsync(
        CancellationToken cancellationToken = default
    ) => await mediator.Send(new GetUserProfileQuery(), cancellationToken);

    /// <summary>
    /// delete user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete()]
    [Authorize()]
    public async Task<ActionResult<Response>> DeleteUserAsync(
        [FromBody] DeleteUserCommand command,
        CancellationToken cancellationToken = default
    ) => await mediator.Send(new GetUserProfileQuery(), cancellationToken);
}
