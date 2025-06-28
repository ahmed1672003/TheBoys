using TheBoys.Application.Features.Users.Commands.Login;

namespace TheBoys.Application.Features.Users.Service;

public interface IUserService
{
    Task<ResponseOf<LoginUserResult>> LoginAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default
    );
}
