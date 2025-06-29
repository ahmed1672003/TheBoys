using TheBoys.Application.Features.Users.Commands.ChangeOtherPassword;
using TheBoys.Application.Features.Users.Queries.Paginate;

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
    /// delete user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete()]
    [Authorize()]
    public async Task<ActionResult<Response>> DeleteUserAsync(
        [FromBody] DeleteUserCommand command,
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
    /// get user profile | used to make current user view his profile
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("profile")]
    [Authorize()]
    public async Task<ActionResult<Response>> GetUserProfileAsync(
        CancellationToken cancellationToken = default
    ) => await mediator.Send(new GetUserProfileQuery(), cancellationToken);

    /// <summary>
    /// paginate all users
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet()]
    [Authorize()]
    public async Task<
        ActionResult<PaginationResponse<List<PaginateUsersResult>>>
    > PaginateUsersAsync(
        [FromQuery] PaginateUsersQuery query,
        CancellationToken cancellationToken = default
    ) => await mediator.Send(query, cancellationToken);

    /// <summary>
    /// change password
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("password")]
    [Authorize()]
    public async Task<ActionResult<Response>> ChangePasswordAsync(
        [FromBody] ChangePasswordCommand command,
        CancellationToken cancellationToken = default
    ) => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// reset other password
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("r-password")]
    [Authorize()]
    public async Task<ActionResult<Response>> ChangeOtherPassword(
        [FromBody] ChangeOtherPasswordCommand command,
        CancellationToken cancellationToken = default
    ) => await mediator.Send(command, cancellationToken);
}
