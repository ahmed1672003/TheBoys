using TheBoys.Application.Features.News.Dtos;

namespace TheBoys.Application.Features.News.Queries.Paginate;

public sealed record PaginateNewsQuery(Guid? OwnerId, bool IsEvent = false, int LanguageId = 2)
    : PaginateQuery,
        IRequest<PaginationResponse<List<NewsDto>>>;
