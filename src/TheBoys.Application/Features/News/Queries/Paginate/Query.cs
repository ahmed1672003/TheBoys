using MediatR;
using TheBoys.API.Dtos;
using TheBoys.Shared.Base.Requests;
using TheBoys.Shared.Base.Responses;

namespace TheBoys.Application.Features.News.Queries.Paginate;

public sealed record PaginateNewsQuery(int LanguageId = 2)
    : PaginateQuery,
        IRequest<PaginationResponse<List<NewsDto>>>;
