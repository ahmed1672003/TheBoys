namespace TheBoys.API.Controllers;

[Route("api/v1/languages")]
[ApiController]
public class LanguageController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// paginate languages
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet()]
    public async Task<ActionResult<List<LanguageDto>>> GetAllAsync(
        [FromQuery] PaginateLanguagesQuery request,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(request, cancellationToken));
}
