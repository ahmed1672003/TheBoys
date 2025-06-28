using TheBoys.Domain.Entities.Roles;

namespace TheBoys.Infrastructure.Repositories;

internal sealed class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(MnfPortalsDbContext context)
        : base(context) { }
}
