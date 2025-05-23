using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheBoys.API.Dtos;
using TheBoys.Application.Features.News.Queries.Handler.Paginate;
using TheBoys.Shared.Base.Responses;

namespace TheBoys.API.Controllers.News;

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
    )
    {
        return Ok(await mediator.Send(request, cancellationToken));
    }

    ///// <summary>
    ///// get news by id
    ///// </summary>
    ///// <remarks>
    ///// route params
    /////
    /////     id => news id | lid => language id
    ///// </remarks>
    ///// <param name="id"></param>
    ///// <param name="lid"></param>
    ///// <param name="cancellationToken"></param>
    ///// <returns></returns>
    //[HttpGet("{id}/{lid}")]
    //public async Task<ActionResult<ResponseOf<NewsDto>>> GetAsync(
    //    [Required][FromRoute] int id,
    //    [Required][FromRoute] int lid,
    //    CancellationToken cancellationToken = default
    //)
    //{
    //    var response = new ResponseOf<NewsDto>();
    //    if (!await _context.NewsTranslations.AnyAsync(x => x.NewsId == id && x.LangId == lid))
    //    {
    //        response.SendBadRequest("No information for news with your language");
    //        return Ok(response);
    //    }
    //    response.Result = await _context
    //        .News.AsNoTracking()
    //        .Include(x => x.NewsTranslations)
    //        .ThenInclude(x => x.Language)
    //        .Where(x => x.NewsId == id)
    //        .Select(news => new NewsDto()
    //        {
    //            Id = news.NewsId,
    //            Date = news.NewsDate,
    //            IsFeatured = news.IsFeatured,
    //            NewsImg = StringExtensions.GetFullPath(news.OwnerId, news.NewsImg),
    //            NewsDetails =
    //                news.NewsTranslations != null && news.NewsTranslations.Any()
    //                    ? news
    //                        .NewsTranslations.Select(t => new NewsTranslationDto()
    //                        {
    //                            Id = t.Id,
    //                            Head = StringExtensions.StripHtml(t.NewsHead),
    //                            Abbr = StringExtensions.StripHtml(t.NewsAbbr),
    //                            Body = StringExtensions.StripHtml(t.NewsBody),
    //                            Source = StringExtensions.StripHtml(t.NewsSource),
    //                            ImgAlt = t.ImgAlt,
    //                            LanguageId = t.LangId,
    //                        })
    //                        .FirstOrDefault(x => x.LanguageId == lid)
    //                    : null,
    //            Languages = news
    //                .NewsTranslations.Select(x => new Entities.LanguageModel()
    //                {
    //                    Id = x.Language.Id,
    //                    Code = x.Language.LCID,
    //                })
    //                .ToList()
    //        })
    //        .FirstOrDefaultAsync(cancellationToken);

    //    foreach (var language in response.Result.Languages)
    //    {
    //        var exactLanguage = StaticLanguages.languageModels.FirstOrDefault(x =>
    //            x.Code.Trim().ToLower() == language.Code.Trim().ToLower()
    //        );

    //        if (exactLanguage is null)
    //            continue;
    //        language.Flag = exactLanguage.Flag;
    //        language.Name = exactLanguage.Name;
    //    }

    //    return Ok(response);
    //}
}
