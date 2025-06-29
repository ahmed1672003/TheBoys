using TheBoys.Domain.Abstractions;

namespace TheBoys.Domain.Entities.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserForLoginAsync(
        string emailOrUserName,
        CancellationToken cancellationToken = default
    );
    Task<User> GetUserForValidatePasswordAsync(
        string emailOrUserName,
        CancellationToken cancellationToken = default
    );
    Task<User> GetUserForValidatePasswordAsync(
        int id,
        CancellationToken cancellationToken = default
    );
    Task<User> GetUserById(int id, CancellationToken cancellationToken = default);
    Task<User> GetUserByIdForUpdateAsync(int id, CancellationToken cancellationToken = default);
    Task<User> GetUserByIdForDeleteAsync(int id, CancellationToken cancellationToken = default);
    //    Task<(List<User> Users, int TotalCount)> PaginateAsync(int pageSize, int pageIndex);
}
