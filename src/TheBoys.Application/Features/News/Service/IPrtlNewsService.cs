using TheBoys.Application.Features.News.Dtos;

namespace TheBoys.Application.Features.News.Service;

public interface IPrtlNewsService
{
    Task<PaginationResponse<List<NewsDto>>> PaginateAsync(
        PaginateNewsQuery query,
        CancellationToken cancellationToken = default
    );
    Task<ResponseOf<NewsDto>> GetAsync(
        GetNewsQuery query,
        CancellationToken cancellationToken = default
    );
}
