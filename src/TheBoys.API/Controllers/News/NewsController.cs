using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBoys.API.Bases.Responses;
using TheBoys.API.Controllers.News.Requests;
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
        [FromQuery] PaginateNewsRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (request.PageSize >= 10)
        {
            request.PageSize = 10;
        }

        var response = new PaginationResponse<List<NewsDto>>();
        var query = _context
            .News.AsNoTracking()
            .Include(x => x.NewsTranslations)
            .ThenInclude(x => x.Language)
            .Select(n => new
            {
                NewsId = n.NewsId,
                NewsDate = n.NewsDate,
                IsFeatured = n.IsFeatured,
                NewsImg = n.NewsImg,
                Translation = n.NewsTranslations.OrderBy(x => x.LangId).FirstOrDefault()
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
            .Where(x => x.Translation != null)
            .Select(x => new NewsDto
            {
                Id = x.NewsId,
                Date = x.NewsDate,
                IsFeatured = x.IsFeatured,
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
            })
            .ToListAsync(cancellationToken);

        if (response.Result is not null && !response.Result.Any())
        {
            foreach (var item in response.Result)
            { }
        }

        response.Count = response.Result.Count;
        response.PageIndex = request.PageIndex;
        response.PageSize = request.PageSize;

        return Ok(response);
    }

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
    )
    {
        var response = new ResponseOf<NewsDto>();
        if (
            !await _context.NewsTranslations.AnyAsync(x =>
                x.NewsId != null && x.NewsId == id && x.LangId == lid
            )
        )
        {
            response.SendBadRequest("No information for news with your language");
            return Ok(response);
        }
        response.Result = await _context
            .News.AsNoTracking()
            .Include(x => x.NewsTranslations)
            .Where(x => x.NewsId == id)
            .Select(news => new NewsDto()
            {
                Id = news.NewsId,
                Date = news.NewsDate,
                IsFeatured = news.IsFeatured,
                NewsImg = news.NewsImg,
                NewsDetails =
                    news.NewsTranslations != null && news.NewsTranslations.Any()
                        ? news
                            .NewsTranslations.Select(t => new NewsTranslationDto()
                            {
                                Id = t.Id,
                                Abbr = t.NewsAbbr,
                                Body = t.NewsBody,
                                Head = t.NewsHead,
                                ImgAlt = t.ImgAlt,
                                LanguageId = t.LangId,
                                Source = t.NewsSource
                            })
                            .FirstOrDefault(x => x.LanguageId == lid)
                        : null,
            })
            .FirstOrDefaultAsync(cancellationToken);

        return Ok(response);
    }
}
