using TheBoys.Infrastructure.Data;

namespace TheBoys.Infrastructure.Repositories;

public sealed class PrtlNewsTranslationRepository
    : Repository<PrtlNewsTranslation>,
        IPrtlNewsTranslationRepository
{
    public PrtlNewsTranslationRepository(MnfPortalsDbContext context)
        : base(context) { }
}
