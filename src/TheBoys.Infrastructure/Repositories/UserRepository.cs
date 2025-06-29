using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using TheBoys.Domain.Entities.Users;

namespace TheBoys.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MnfPortalsDbContext context)
        : base(context) { }

    public async Task<User> GetUserForLoginAsync(
        string emailOrUserName,
        CancellationToken cancellationToken = default
    )
    {
        Expression<Func<User, bool>> filter = (User user) =>
            new EmailAddressAttribute().IsValid(emailOrUserName)
                ? user.Email.ToLower() == emailOrUserName.ToLower()
                : user.Username.ToLower() == emailOrUserName;

        return await _entities.Include(x => x.Role).FirstAsync(filter);
    }

    public async Task<User> GetUserForValidatePasswordAsync(
        string emailOrUserName,
        CancellationToken cancellationToken = default
    )
    {
        Expression<Func<User, bool>> filter = (User user) =>
            new EmailAddressAttribute().IsValid(emailOrUserName)
                ? user.Email.ToLower() == emailOrUserName.ToLower()
                : user.Username.ToLower() == emailOrUserName;

        return await _entities.FirstAsync(filter);
    }

    public Task<User> GetUserForValidatePasswordAsync(
        int id,
        CancellationToken cancellationToken = default
    ) => _entities.FirstAsync(x => x.Id == id, cancellationToken);

    public async Task<User> GetUserById(int id, CancellationToken cancellationToken = default) =>
        await _entities
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<User> GetUserByIdForUpdateAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        return _entities.FirstAsync(x => x.Id == id);
    }
}
