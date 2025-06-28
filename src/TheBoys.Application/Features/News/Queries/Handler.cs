using TheBoys.Application.Features.News.Queries.GetNewsDetails;

namespace TheBoys.Application.Features.News.Queries;

internal sealed class PrtlNewsQueriesHandler(IPrtlNewsService prtlNewsService)
    : IRequestHandler<PaginateNewsQuery, PaginationResponse<List<NewsDto>>>,
        IRequestHandler<GetNewsQuery, ResponseOf<NewsDto>>,
        IRequestHandler<GetNewsDetailsQuery, ResponseOf<GetNewsDetailsResult>>
{
    public async Task<PaginationResponse<List<NewsDto>>> Handle(
        PaginateNewsQuery request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.PaginateAsync(request, cancellationToken);

    public async Task<ResponseOf<NewsDto>> Handle(
        GetNewsQuery request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.GetAsync(request, cancellationToken);

    public async Task<ResponseOf<GetNewsDetailsResult>> Handle(
        GetNewsDetailsQuery request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.GetNewsDetailsAsync(request.Id, cancellationToken);
}
