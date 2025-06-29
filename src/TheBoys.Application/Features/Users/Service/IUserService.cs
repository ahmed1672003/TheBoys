using TheBoys.Application.Features.Users.Queries.GetById;
using TheBoys.Application.Features.Users.Queries.GetProfile;
using TheBoys.Application.Features.Users.Queries.Paginate;

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
        int userId,
        string newPassword,
        CancellationToken cancellationToken = default
    );
    Task<Response> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<PaginationResponse<List<PaginateUsersResult>>> PaginateAsync(
        PaginateUsersQuery query,
        CancellationToken cancellationToken = default
    );
}
