using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using TheBoys.Contracts.Users;
using TheBoys.Domain.Entities.Users;
using TheBoys.Shared.Extensions;

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

    public async Task<User> GetUserByIdForUpdateAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        return await _entities.FirstAsync(x => x.Id == id);
    }

    public async Task<User> GetUserByIdForDeleteAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        return await _entities.FirstAsync(x => x.Id == id);
    }

    public async Task<UsersPaginationContract> PaginateAsync(
        PaginateUsersContract contract,
        CancellationToken cancellationToken = default
    )
    {
        var usersContract = new UsersPaginationContract();
        var query = _entities.AsNoTracking();

        if (contract.Search.HasValue())
            query = query.Where(x =>
                EF.Functions.Like(x.Name, $"%{x.Name}%")
                || EF.Functions.Like(x.Email, $"%{x.Email}%")
            );

        if (contract.RoleId.HasValue)
        {
            query = query.Where(x => x.RoleId == contract.RoleId);
        }
        usersContract.TotalCount = await query.CountAsync(cancellationToken);

        usersContract.Elements = await query
            .Include(x => x.Role)
            .Paginate(contract.PageIndex, contract.PageSize)
            .Select(x => new UsersPaginationContract.UserContract()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Username = x.Username,
                RoleId = x.RoleId,
                Role = new UsersPaginationContract.UserContract.RoleContract()
                {
                    Id = x.Role.Id,
                    Type = x.Role.Type,
                },
            })
            .ToListAsync(cancellationToken);

        return usersContract;
    }
}
