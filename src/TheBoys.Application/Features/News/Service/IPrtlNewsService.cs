using TheBoys.Application.Features.News.Commands.Handler.Create;
using TheBoys.Application.Features.News.Commands.Handler.Delete;
using TheBoys.Application.Features.News.Commands.Handler.Update;

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
    Task<Response> CreateAsync(
        CreateNewsCommand command,
        CancellationToken cancellationToken = default
    );
    Task<Response> DeleteAsync(
        DeleteNewsCommand command,
        CancellationToken cancellationToken = default
    );
    Task<Response> UpdateAsync(
        UpdateNewsCommand command,
        CancellationToken cancellationToken = default
    );
}
