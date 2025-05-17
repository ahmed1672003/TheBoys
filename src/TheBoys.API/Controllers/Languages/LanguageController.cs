using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBoys.API.Base.Requests;
using TheBoys.API.Bases.Responses;
using TheBoys.API.Data;
using TheBoys.API.Dtos;
using TheBoys.API.Extensions;
using TheBoys.API.Misc;

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
    [HttpGet()]
    public async Task<ActionResult<List<LanguageDto>>> GetAllAsync(
        [FromQuery] PaginateRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var response = new PaginationResponse<List<LanguageDto>>()
        {
            Result = new List<LanguageDto>()
        };

        var query = _context.Languages.AsNoTracking().OrderBy(x => x.LCID.ToLower()).AsQueryable();

        response.TotalCount = await query.CountAsync(cancellationToken);

        query = query.Paginate(request.PageIndex, request.PageSize);

        response.PageIndex = request.PageIndex;
        response.PageSize = request.PageSize;
        var languages = await query.ToListAsync(cancellationToken);

        foreach (var language in StaticLanguages.languageModels)
        {
            var lang = languages.FirstOrDefault(x =>
                x.LCID.Trim().ToLower() == language.Code.Trim().ToLower()
            );

            if (lang is null)
                continue;

            response.Result.Add(
                new LanguageDto()
                {
                    Id = lang.Id,
                    Name = language.Name,
                    Code = language.Code,
                    Flag = language.Flag
                }
            );
        }
        response.Count = response.Result.Count;
        response.SendSuccess();
        return Ok(response);
    }
}
