using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBoys.API.Base.Requests;
using TheBoys.API.Bases.Responses;
using TheBoys.API.Data;
using TheBoys.API.Dtos;
using TheBoys.API.Extensions;

namespace TheBoys.API.Controllers.News;

[Route("api/v1/news")]
[ApiController]
public class NewsController : ControllerBase
{
    readonly ApplicationDbContext _context;

    public NewsController(ApplicationDbContext context) => _context = context;

    /// <summary>
    /// paginate all news
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet()]
    public async Task<ActionResult<PaginationResponse<List<NewsDto>>>> PaginateAllNewsAsync(
        [FromQuery] PaginateRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (request.PageSize >= 15)
        {
            request.PageSize = 15;
        }

        var response = new PaginationResponse<List<NewsDto>>();
        var query = _context
            .News.AsNoTracking()
            .Select(n => new
            {
                NewsId = n.NewsId,
                NewsDate = n.NewsDate,
                IsFeature = n.IsFeature,
                NewsImg = n.NewsImg,
                Translation = n.NewsTranslations.OrderBy(t => t.Id).FirstOrDefault(),
                Images = n.NewsImages
            });

        if (request.Search.HasValue())
        {
            query = query.Where(x =>
                EF.Functions.Like(x.Translation.NewsHead, $"%{request.Search}%")
                || EF.Functions.Like(x.Translation.NewsBody, $"%{request.Search}%")
                || EF.Functions.Like(x.Translation.NewsAbbr, $"%{request.Search}%")
                || EF.Functions.Like(x.Translation.NewsSource, $"%{request.Search}%")
            );
        }

        response.TotalCount = await query.CountAsync(cancellationToken);

        response.Result = await query
            .OrderByDescending(x => x.NewsDate)
            .Paginate(request.PageIndex, request.PageSize)
            .Select(x => new NewsDto
            {
                Id = x.NewsId,
                Date = x.NewsDate,
                IsFeature = x.IsFeature,
                NewsImg = x.NewsImg,
                NewsDetails =
                    x.Translation == null
                        ? null!
                        : new NewsTranslationDto
                        {
                            Id = x.Translation.Id,
                            LanguageId = x.Translation.LangId,
                            Head = x.Translation.NewsHead,
                            Abbr = x.Translation.NewsAbbr,
                            Body = x.Translation.NewsBody,
                            Source = x.Translation.NewsSource,
                            ImgAlt = x.Translation.ImgAlt
                        },
                Images = x
                    .Images.Select(img => new NewsImageDto { Id = img.Id, Url = img.NewsUrl })
                    .ToList()
            })
            .ToListAsync(cancellationToken);
        response.Count = response.Result.Count;
        response.PageIndex = request.PageIndex;
        response.PageSize = request.PageSize;

        return Ok(response);
    }
}
