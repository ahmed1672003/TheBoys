using TheBoys.Application.Features.Users.Commands.AddUser;
using TheBoys.Application.Features.Users.Commands.ChangePassword;
using TheBoys.Application.Features.Users.Commands.Login;
using TheBoys.Application.Features.Users.Commands.Update;
using TheBoys.Application.Features.Users.Queries.GetById;

namespace TheBoys.Application.Features.Users.Service;

public interface IUserService
{
    Task<ResponseOf<AuthUserResult>> LoginAsync(
        AuthUserCommand command,
        CancellationToken cancellationToken = default
    );
    Task<Response> AddUserAsync(
        AddUserCommand command,
        CancellationToken cancellationToken = default
    );

    Task<Response> UpdateAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken = default
    );
    Task<ResponseOf<GetUserByIdResult>> GetUserByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    );
    Task<ResponseOf<GetUserProfileResult>> GetUserProfileAsync(
        int id,
        CancellationToken cancellationToken = default
    );
    Task<Response> ChangePasswordAsync(
        ChangePasswordCommand command,
        int userId,
        CancellationToken cancellationToken = default
    );
    Task<Response> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
