using TheBoys.Application.Features.News.Dtos;

namespace TheBoys.Application.Features.News.Queries;

internal sealed class PrtlNewsQueriesHandler(IPrtlNewsService prtlNewsService)
    : IRequestHandler<PaginateNewsQuery, PaginationResponse<List<NewsDto>>>,
        IRequestHandler<GetNewsQuery, ResponseOf<NewsDto>>
{
    public async Task<PaginationResponse<List<NewsDto>>> Handle(
        PaginateNewsQuery request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.PaginateAsync(request, cancellationToken);

    public async Task<ResponseOf<NewsDto>> Handle(
        GetNewsQuery request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.GetAsync(request, cancellationToken);
}
