using TheBoys.Contracts.News;
using TheBoys.Domain.Abstractions;

namespace TheBoys.Domain.Entities.News;

public interface IPrtlNewsRepository : IRepository<PrtlNews>
{
    Task<NewsPaginationContarct> PaginateAsync(
        PaginateNewsContract contract,
        CancellationToken cancellationToken = default
    );
}
