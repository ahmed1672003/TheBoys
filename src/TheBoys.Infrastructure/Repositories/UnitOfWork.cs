using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using TheBoys.Domain.Abstractions;
using TheBoys.Infrastructure.Data;

namespace TheBoys.Infrastructure.Repositories;

public sealed class UnitOfWork(MnfPortalsDbContext context) : IUnitOfWork
{
    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default
    ) => context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

    public Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default
    ) => context.Database.BeginTransactionAsync(cancellationToken);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);

    public async Task<bool> SaveChangesAsync(
        int modifiedRows,
        CancellationToken cancellationToken = default
    ) => (await context.SaveChangesAsync(cancellationToken)) == modifiedRows;
}
