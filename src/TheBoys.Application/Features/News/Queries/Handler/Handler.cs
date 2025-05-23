using MediatR;
using TheBoys.API.Dtos;
using TheBoys.Application.Features.News.Queries.Handler.Paginate;
using TheBoys.Application.Features.News.Service;
using TheBoys.Shared.Base.Responses;

namespace TheBoys.Application.Features.News.Queries.Handler;

internal sealed class PrtlNewsQueriesHandler(IPrtlNewsService prtlNewsService)
    : IRequestHandler<PaginateNewsQuery, PaginationResponse<List<NewsDto>>>
{
    public async Task<PaginationResponse<List<NewsDto>>> Handle(
        PaginateNewsQuery request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.PaginateAsync(request, cancellationToken);
}
