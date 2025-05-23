namespace TheBoys.Application.Features.Languages.Queries.Pagiante;

public sealed record PaginateLanguagesQuery
    : PaginateQuery,
        IRequest<PaginationResponse<List<LanguageModel>>>;
