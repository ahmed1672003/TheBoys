using TheBoys.Application.Features.Roles.Queries.GetAll;

namespace TheBoys.API.Controllers;

[Route("api/v1/role")]
[ApiController]
public class RoleController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///  get all roles
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ResponseOf<List<GetAllRolesResult>>>> GetAllAsync(
        CancellationToken cancellationToken = default
    ) => await mediator.Send(new GetAllRolesQuery(), cancellationToken);
}
