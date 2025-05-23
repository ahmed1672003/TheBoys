namespace TheBoys.Application.Features.Languages.Service;

public interface IPrtlLanguageService
{
    Task<PaginationResponse<List<LanguageModel>>> PaginateAsync(
        PaginateQuery query,
        CancellationToken cancellationToken = default
    );
}
