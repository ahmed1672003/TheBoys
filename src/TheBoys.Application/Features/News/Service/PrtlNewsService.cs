using TheBoys.Application.Features.News.Dtos;
using TheBoys.Contracts.News;

namespace TheBoys.Application.Features.News.Service;

public sealed class PrtlNewsService(IPrtlNewsRepository prtlNewsRepository) : IPrtlNewsService
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
                OwnerId = query.OwnerId
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
            new GetNewsContract() { NewsId = query.Id, LanguageId = query.LanguageId, },
            cancellationToken
        );
        return new ResponseOf<NewsDto>() { Result = contract, Success = true, };
    }
}
