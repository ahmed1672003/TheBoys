using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace TheBoys.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default
    );
    Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default
    );

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
    Task<bool> SaveChangesAsync(int modifiedRows, CancellationToken cancellationToken = default);
}
