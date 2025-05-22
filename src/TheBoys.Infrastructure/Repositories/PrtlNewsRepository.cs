using TheBoys.Infrastructure.Data;

namespace TheBoys.Infrastructure.Repositories;

public sealed class PrtlNewsRepository : Repository<PrtlNews>, IPrtlNewsRepository
{
    public PrtlNewsRepository(MnfPortalsDbContext context)
        : base(context) { }
}
