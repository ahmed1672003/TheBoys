namespace TheBoys.Application.Features.Languages.Queries;

internal sealed class PrtlLangaugeQueriesHandler(IPrtlLanguageService prtlLanguageService)
    : IRequestHandler<PaginateLanguagesQuery, PaginationResponse<List<LanguageModel>>>
{
    public async Task<PaginationResponse<List<LanguageModel>>> Handle(
        PaginateLanguagesQuery request,
        CancellationToken cancellationToken
    ) => await prtlLanguageService.PaginateAsync(request, cancellationToken);
}
