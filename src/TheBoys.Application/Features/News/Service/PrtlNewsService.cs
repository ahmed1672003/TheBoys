using TheBoys.Application.Features.News.Commands.Handler.Create;
using TheBoys.Application.Features.News.Commands.Handler.Delete;
using TheBoys.Application.Features.News.Commands.Handler.Update;
using TheBoys.Application.Features.News.Queries.GetNewsDetails;
using TheBoys.Contracts.News;
using TheBoys.Domain.Abstractions;

namespace TheBoys.Application.Features.News.Service;

public sealed class PrtlNewsService(IPrtlNewsRepository prtlNewsRepository, IUnitOfWork unitOfWork)
    : IPrtlNewsService
{
    public async Task<PaginationResponse<List<NewsDto>>> PaginateAsync(
        PaginateNewsQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var contract = await prtlNewsRepository.PaginateAsync(
            new PaginateNewsContract()
            {
                LanguageId = query.LanguageId,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize > 10 ? 10 : query.PageSize,
                Search = query.Search,
                OwnerId = query.OwnerId,
            },
            cancellationToken
        );

        var response = new PaginationResponse<List<NewsDto>>()
        {
            PageSize = query.PageSize,
            PageIndex = query.PageIndex,
            Result = new(),
            Count = contract.Elements.Count,
            TotalCount = contract.TotalCount,
        };

        foreach (var element in contract.Elements)
            response.Result.Add(element);

        return response;
    }

    public async Task<ResponseOf<NewsDto>> GetAsync(
        GetNewsQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var contract = await prtlNewsRepository.GetAsync(
            new GetNewsContract() { NewsId = query.Id, LanguageId = query.LanguageId },
            cancellationToken
        );
        return new ResponseOf<NewsDto>() { Result = contract, Success = true };
    }

    public async Task<Response> CreateAsync(
        CreateNewsCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var news = new PrtlNews
        {
            IsFeatured = command.IsFeatured,
            NewsDate = DateTime.Now,
            NewsImg = command.NewsImg,
            OwnerId = command.OwnerId,
            Published = command.Published,
            PrtlNewsTranslations = command
                .Translations.Select(x => new PrtlNewsTranslation()
                {
                    ImgAlt = x.ImgAlt,
                    LangId = x.LangId,
                    NewsAbbr = x.NewsAbbr,
                    NewsBody = x.NewsBody,
                    NewsHead = x.NewsHead,
                    NewsSource = x.NewsSource,
                })
                .ToList(),
        };
        var entity = await prtlNewsRepository.CreateAsync(news, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new Response() { Success = true };
    }

    public async Task<Response> DeleteAsync(
        DeleteNewsCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var news = await prtlNewsRepository.GetByIdForDeleteAsync(command.Id, cancellationToken);
        await prtlNewsRepository.DeleteAsync(news, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new Response() { Success = true };
    }

    public async Task<Response> UpdateAsync(
        UpdateNewsCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var news = await prtlNewsRepository.GetByIdForUpdateAsync(command.Id, cancellationToken);
        news.IsFeatured = command.IsFeatured;
        news.Published = command.Published;
        news.OwnerId = command.OwnerId;
        news.NewsImg = command.NewsImg;
        news.PrtlNewsTranslations.Clear();

        foreach (var translation in command.Translations)
        {
            news.PrtlNewsTranslations.Add(
                new PrtlNewsTranslation()
                {
                    Id = translation.Id.HasValue ? translation.Id.Value : default,
                    ImgAlt = translation.ImgAlt,
                    LangId = translation.LangId,
                    NewsAbbr = translation.NewsAbbr,
                    NewsBody = translation.NewsBody,
                    NewsHead = translation.NewsHead,
                    NewsSource = translation.NewsSource,
                }
            );
        }
        await prtlNewsRepository.UpdateAsync(news, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new Response() { Success = true };
    }

    public async Task<ResponseOf<GetNewsDetailsResult>> GetNewsDetailsAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        var news = await prtlNewsRepository.GetNewsDetailsAsync(id, cancellationToken);
        var result = new GetNewsDetailsResult()
        {
            Id = news.NewsId,
            Date = news.NewsDate,
            IsFeatured = news.IsFeatured,
            NewsImg = news.NewsImg,
            OwnerId = news.OwnerId,
            Published = news.Published,
            Translations = news
                .PrtlNewsTranslations.Select(t => new NewsTranslationDto()
                {
                    Id = t.Id,
                    Abbr = t.NewsAbbr,
                    Body = t.NewsBody,
                    Head = t.NewsHead,
                    ImgAlt = t.ImgAlt,
                    LanguageId = t.LangId,
                    Source = t.NewsSource,
                    Language = StaticLanguages.languageModels.FirstOrDefault(x =>
                        x.Code.ToLower() == t.Lang.Lcid.ToLower()
                    ),
                })
                .ToList(),
        };

        return new ResponseOf<GetNewsDetailsResult>() { Success = true, Result = result };
    }
}
