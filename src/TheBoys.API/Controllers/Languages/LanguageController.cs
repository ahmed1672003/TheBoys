using Microsoft.AspNetCore.Mvc;
using TheBoys.API.Data;

namespace TheBoys.API.Controllers.Languages;

[Route("api/v1/languages")]
[ApiController]
public class LanguageController : ControllerBase
{
    readonly ApplicationDbContext _context;

    public LanguageController(ApplicationDbContext context) => _context = context;

    /// <summary>
    /// paginate languages
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    //[HttpGet()]
    //public async Task<ActionResult<List<LanguageDto>>> GetAllAsync(
    //    [FromQuery] PaginateRequest request,
    //    CancellationToken cancellationToken = default
    //)
    //{
    //    var response = new PaginationResponse<List<LanguageDto>>();

    //    var query = _context.Languages.AsNoTracking().OrderBy(x => x.Name.ToLower()).AsQueryable();

    //    response.TotalCount = await query.CountAsync(cancellationToken);

    //    query = query.Paginate(request.PageIndex, request.PageSize);

    //    response.PageIndex = request.PageIndex;
    //    response.PageSize = request.PageSize;
    //    response.Result = await query
    //        .Select(l => new LanguageDto()
    //        {
    //            Id = l.Id,
    //            Code = l.Code,
    //            Flag = l.Flag,
    //            Name = l.Name
    //        })
    //        .ToListAsync(cancellationToken);
    //    response.SendSuccess();
    //    return Ok(response);
    //}
}
