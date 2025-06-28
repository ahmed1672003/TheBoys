using TheBoys.Contracts.News;
using TheBoys.Domain.Abstractions;

namespace TheBoys.Domain.Entities.News;

public interface IPrtlNewsRepository : IRepository<PrtlNews>
{
    Task<NewsPaginationContarct> PaginateAsync(
        PaginateNewsContract contract,
        CancellationToken cancellationToken = default
    );
    Task<NewsPaginationContarct.NewsContract> GetAsync(
        GetNewsContract contract,
        CancellationToken cancellationToken = default
    );
    Task<PrtlNews> GetByIdForDeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<PrtlNews> GetByIdForUpdateAsync(int id, CancellationToken cancellationToken = default);

    Task<PrtlNews> GetNewsDetailsAsync(int id, CancellationToken cancellationToken = default);
}
