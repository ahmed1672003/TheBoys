using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TheBoys.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly MnfPortalsDbContext _context;
    protected readonly DbSet<TEntity> _entities;

    public Repository(MnfPortalsDbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public ValueTask<EntityEntry<TEntity>> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => _entities.AddAsync(entity, cancellationToken);

    public ValueTask<EntityEntry<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => ValueTask.FromResult(_entities.Update(entity));

    public ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _entities.Remove(entity);
        return ValueTask.CompletedTask;
    }

    public Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    )
    {
        var query = _entities.AsQueryable();

        if (filter is not null)
        {
            return query.AnyAsync(filter, cancellationToken);
        }

        return query.AnyAsync(cancellationToken);
    }
}
