using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheBoys.Domain.Abstractions;
using TheBoys.Infrastructure.Data;

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
}
