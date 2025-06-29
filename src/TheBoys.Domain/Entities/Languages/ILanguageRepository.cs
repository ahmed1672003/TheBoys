using TheBoys.Contracts;
using TheBoys.Contracts.Languages;
using TheBoys.Domain.Abstractions;

namespace TheBoys.Domain.Entities.Languages;

public interface IPrtlLanguageRepository : IRepository<PrtlLanguage>
{
    Task<LanguagesPaginationContract> PaginateAsync(
        PaginateContract contract,
        CancellationToken cancellationToken = default
    );
}
