using TheBoys.Domain.Entities.Users;

namespace TheBoys.Application.Abstractions;

public interface IJwtManager
{
    Task<string> GenerateTokenAsync(User user, CancellationToken cancellationToken = default);
}
