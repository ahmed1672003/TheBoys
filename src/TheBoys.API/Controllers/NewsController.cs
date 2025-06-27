using TheBoys.Application.Features.News.Commands.Handler.Create;
using TheBoys.Application.Features.News.Commands.Handler.Delete;
using TheBoys.Application.Features.News.Commands.Handler.Update;

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
        [Required] [FromRoute] int id,
        [Required] [FromRoute] int lid,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new GetNewsQuery(id, lid), cancellationToken));

    /// <summary>
    /// create news
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<ActionResult<Response>> CreateAsync(
        [FromBody] CreateNewsCommand command,
        CancellationToken cancellationToken
    ) => Ok(await mediator.Send(command, cancellationToken));

    /// <summary>
    /// update news
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut()]
    public async Task<ActionResult<Response>> CreateAsync(
        [FromBody] UpdateNewsCommand command,
        CancellationToken cancellationToken
    ) => Ok(await mediator.Send(command, cancellationToken));

    /// <summary>
    /// delete news
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete()]
    public async Task<ActionResult<Response>> CreateAsync(
        [FromBody] DeleteNewsCommand command,
        CancellationToken cancellationToken
    ) => Ok(await mediator.Send(command, cancellationToken));
}
