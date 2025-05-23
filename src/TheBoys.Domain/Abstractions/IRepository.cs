using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TheBoys.Domain.Abstractions;

public interface IRepository<TEntity>
    where TEntity : class
{
    ValueTask<EntityEntry<TEntity>> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );
    ValueTask<EntityEntry<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    );
}
