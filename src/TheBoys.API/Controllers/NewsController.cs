using Microsoft.AspNetCore.Authorization;
using TheBoys.Application.Features.News.Commands.Create;
using TheBoys.Application.Features.News.Commands.Handler.Delete;
using TheBoys.Application.Features.News.Commands.Update;
using TheBoys.Application.Features.News.Dtos;
using TheBoys.Application.Features.News.Queries.GetNewsDetails;

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
    [AllowAnonymous]
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
    [AllowAnonymous]
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
    [Authorize()]
    public async Task<ActionResult<Response>> CreateAsync(
        [FromBody] CreateNewsCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    /// <summary>
    /// update news
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update")]
    [Authorize()]
    public async Task<ActionResult<Response>> UpdateAsync(
        [FromBody] UpdateNewsCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    /// <summary>
    /// delete news
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("delete/{id}")]
    [Authorize()]
    public async Task<ActionResult<Response>> DeleteAsync(
        [Required] [FromRoute] int id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new DeleteNewsCommand(id), cancellationToken));

    /// <summary>
    /// get news with full translations
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize()]
    public async Task<ActionResult<Response>> GetNewsDtailsAsync(
        [Required] [FromRoute] int id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new GetNewsDetailsQuery(id), cancellationToken));
}
