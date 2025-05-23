using TheBoys.API.Dtos;
using TheBoys.Application.Features.News.Queries.Handler.Paginate;
using TheBoys.Shared.Base.Responses;

namespace TheBoys.Application.Features.News.Service;

public interface IPrtlNewsService
{
    Task<PaginationResponse<List<NewsDto>>> PaginateAsync(
        PaginateNewsQuery query,
        CancellationToken cancellationToken = default
    );
}
