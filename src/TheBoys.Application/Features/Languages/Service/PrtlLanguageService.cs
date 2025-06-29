using TheBoys.Contracts;
using TheBoys.Domain.Entities.Languages;

namespace TheBoys.Application.Features.Languages.Service;

public sealed class PrtlLanguageService(IPrtlLanguageRepository prtlLanguageRepository)
    : IPrtlLanguageService
{
    public async Task<PaginationResponse<List<LanguageModel>>> PaginateAsync(
        PaginateQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var contract = await prtlLanguageRepository.PaginateAsync(
            new PaginateContract() { PageIndex = query.PageIndex, PageSize = query.PageSize }
        );
        var response = new PaginationResponse<List<LanguageModel>>()
        {
            PageSize = query.PageSize > 10 ? 10 : query.PageSize,
            PageIndex = query.PageIndex,
            Count = contract.Elements.Count,
            TotalCount = contract.TotalCount,
            Result = new(),
        };

        foreach (var language in StaticLanguages.languageModels)
        {
            var lang = contract.Elements.FirstOrDefault(x =>
                x.Code.Trim().ToLower() == language.Code.Trim().ToLower()
            );

            if (lang is null)
                continue;

            response.Result.Add(
                new LanguageModel()
                {
                    Id = lang.Id,
                    Name = language.Name,
                    Code = language.Code,
                    Flag = language.Flag,
                }
            );
        }

        return response;
    }
}
