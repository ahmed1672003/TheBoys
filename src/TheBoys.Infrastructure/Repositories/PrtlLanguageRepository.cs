using TheBoys.Contracts;
using TheBoys.Contracts.Languages;
using TheBoys.Shared.Extensions;
using TheBoys.Shared.Misc;

namespace TheBoys.Infrastructure.Repositories;

public sealed class PrtlLanguageRepository : Repository<PrtlLanguage>, IPrtlLanguageRepository
{
    public PrtlLanguageRepository(MnfPortalsDbContext context)
        : base(context) { }

    public async Task<LanguagesPaginationContract> PaginateAsync(
        PaginateContract contract,
        CancellationToken cancellationToken = default
    )
    {
        var resultContract = new LanguagesPaginationContract();

        var query = _entities.AsNoTracking().OrderBy(x => x.Lcid.ToLower()).AsQueryable();

        resultContract.TotalCount = await query.CountAsync(cancellationToken);
        query = query.Paginate(contract.PageIndex, contract.PageSize);

        resultContract.Elements = await query
            .Select(lang => new LanguageModel() { Id = lang.LangId, Code = lang.Lcid })
            .ToListAsync(cancellationToken);

        return resultContract;
    }
}
