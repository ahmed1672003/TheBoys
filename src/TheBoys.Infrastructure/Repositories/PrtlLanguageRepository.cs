using TheBoys.Infrastructure.Data;

namespace TheBoys.Infrastructure.Repositories;

public sealed class PrtlLanguageRepository : Repository<PrtlLanguage>, IPrtlLanguageRepository
{
    public PrtlLanguageRepository(MnfPortalsDbContext context)
        : base(context) { }
}
