//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using TheBoys.API.Bases.Responses;
//using TheBoys.API.Controllers.News.Requests;
//using TheBoys.API.Data;
//using TheBoys.API.Dtos;
//using TheBoys.API.Extensions;
//using TheBoys.API.Misc;

//namespace TheBoys.API.Controllers.News;

//[Route("api/v1/news")]
//[ApiController]
//public class NewsController : ControllerBase
//{
//    readonly ApplicationDbContext _context;

//    public NewsController(ApplicationDbContext context) => _context = context;

//    /// <summary>
//    /// paginate all news
//    /// </summary>
//    /// <param name="request"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    [HttpGet()]
//    public async Task<ActionResult<PaginationResponse<List<NewsDto>>>> PaginateAllNewsAsync(
//        [FromQuery] PaginateNewsRequest request,
//        CancellationToken cancellationToken = default
//    )
//    {
//        if (request.PageSize >= 10)
//        {
//            request.PageSize = 10;
//        }

//        var response = new PaginationResponse<List<NewsDto>>();
//        var query = _context
//            .News.AsNoTracking()
//            .Include(x => x.NewsTranslations)
//            .ThenInclude(x => x.Language)
//            .Where(x =>
//                x.Published
//                && x.NewsTranslations != null
//                && x.NewsTranslations.Any(x => x.LangId == request.LanguageId)
//            )
//            .Select(n => new
//            {
//                NewsId = n.NewsId,
//                NewsDate = n.NewsDate,
//                IsFeatured = n.IsFeatured,
//                NewsImg = n.NewsImg,
//                OwnerId = n.OwnerId,
//                Translation = n.NewsTranslations.FirstOrDefault(x => x.LangId == request.LanguageId)
//            });

//        if (request.Search.HasValue())
//        {
//            query = query.Where(x =>
//                EF.Functions.Like(x.Translation.NewsHead, $"{request.Search}%")
//            );
//        }

//        response.TotalCount = await query.CountAsync(cancellationToken);

//        response.Result = await query
//            .AsSplitQuery()
//            .OrderByDescending(x => x.NewsDate)
//            .Paginate(request.PageIndex, request.PageSize)
//            .Where(x => x.Translation != null)
//            .Select(x => new NewsDto
//            {
//                Id = x.NewsId,
//                Date = x.NewsDate,
//                IsFeatured = x.IsFeatured,
//                NewsImg = StringExtensions.GetFullPath(x.OwnerId, x.NewsImg),
//                NewsDetails =
//                    x.Translation == null
//                        ? null!
//                        : new NewsTranslationDto
//                        {
//                            Id = x.Translation.Id,
//                            LanguageId = x.Translation.LangId,
//                            Head = StringExtensions.StripHtml(x.Translation.NewsHead),
//                            Abbr = StringExtensions.StripHtml(x.Translation.NewsAbbr),
//                            Body = StringExtensions.StripHtml(x.Translation.NewsBody),
//                            Source = StringExtensions.StripHtml(x.Translation.NewsSource),
//                            ImgAlt = x.Translation.ImgAlt
//                        },
//            })
//            .ToListAsync(cancellationToken);

//        response.Count = response.Result.Count;
//        response.PageIndex = request.PageIndex;
//        response.PageSize = request.PageSize;

//        return Ok(response);
//    }

//    /// <summary>
//    /// get news by id
//    /// </summary>
//    /// <remarks>
//    /// route params
//    ///
//    ///     id => news id | lid => language id
//    /// </remarks>
//    /// <param name="id"></param>
//    /// <param name="lid"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    [HttpGet("{id}/{lid}")]
//    public async Task<ActionResult<ResponseOf<NewsDto>>> GetAsync(
//        [Required][FromRoute] int id,
//        [Required][FromRoute] int lid,
//        CancellationToken cancellationToken = default
//    )
//    {
//        var response = new ResponseOf<NewsDto>();
//        if (!await _context.NewsTranslations.AnyAsync(x => x.NewsId == id && x.LangId == lid))
//        {
//            response.SendBadRequest("No information for news with your language");
//            return Ok(response);
//        }
//        response.Result = await _context
//            .News.AsNoTracking()
//            .Include(x => x.NewsTranslations)
//            .ThenInclude(x => x.Language)
//            .Where(x => x.NewsId == id)
//            .Select(news => new NewsDto()
//            {
//                Id = news.NewsId,
//                Date = news.NewsDate,
//                IsFeatured = news.IsFeatured,
//                NewsImg = StringExtensions.GetFullPath(news.OwnerId, news.NewsImg),
//                NewsDetails =
//                    news.NewsTranslations != null && news.NewsTranslations.Any()
//                        ? news
//                            .NewsTranslations.Select(t => new NewsTranslationDto()
//                            {
//                                Id = t.Id,
//                                Head = StringExtensions.StripHtml(t.NewsHead),
//                                Abbr = StringExtensions.StripHtml(t.NewsAbbr),
//                                Body = StringExtensions.StripHtml(t.NewsBody),
//                                Source = StringExtensions.StripHtml(t.NewsSource),
//                                ImgAlt = t.ImgAlt,
//                                LanguageId = t.LangId,
//                            })
//                            .FirstOrDefault(x => x.LanguageId == lid)
//                        : null,
//                Languages = news
//                    .NewsTranslations.Select(x => new Entities.LanguageModel()
//                    {
//                        Id = x.Language.Id,
//                        Code = x.Language.LCID,
//                    })
//                    .ToList()
//            })
//            .FirstOrDefaultAsync(cancellationToken);

//        foreach (var language in response.Result.Languages)
//        {
//            var exactLanguage = StaticLanguages.languageModels.FirstOrDefault(x =>
//                x.Code.Trim().ToLower() == language.Code.Trim().ToLower()
//            );

//            if (exactLanguage is null)
//                continue;
//            language.Flag = exactLanguage.Flag;
//            language.Name = exactLanguage.Name;
//        }

//        return Ok(response);
//    }
//}
