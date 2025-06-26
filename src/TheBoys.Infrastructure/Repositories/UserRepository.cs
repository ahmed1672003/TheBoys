using TheBoys.Domain.Entities.Users;

namespace TheBoys.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MnfPortalsDbContext context)
        : base(context) { }
}
