namespace TheBoys.API.Controllers;

[Route("api/v1/news")]
[ApiController]
public class NewsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// paginate all news
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet()]
    public async Task<ActionResult<PaginationResponse<List<NewsDto>>>> PaginateAllNewsAsync(
        [FromQuery] PaginateNewsQuery request,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(request, cancellationToken));

    /// <summary>
    /// get news by id
    /// </summary>
    /// <remarks>
    /// route params
    ///
    ///     id => news id | lid => language id
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="lid"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}/{lid}")]
    public async Task<ActionResult<ResponseOf<NewsDto>>> GetAsync(
        [Required][FromRoute] int id,
        [Required][FromRoute] int lid,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new GetNewsQuery(id, lid), cancellationToken));
}
