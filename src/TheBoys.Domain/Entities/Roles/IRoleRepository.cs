using System.Linq.Expressions;
using TheBoys.Domain.Abstractions;

namespace TheBoys.Domain.Entities.Roles;

public interface IRoleRepository : IRepository<Role>
{
    Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default);
    Role Get(Expression<Func<Role, bool>> filter);
}
