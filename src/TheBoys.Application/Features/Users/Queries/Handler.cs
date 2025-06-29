using TheBoys.Application.Features.Users.Queries.GetById;
using TheBoys.Application.Features.Users.Queries.GetProfile;
using TheBoys.Application.Features.Users.Queries.Paginate;

namespace TheBoys.Application.Features.Users.Queries;

internal sealed class UserQueriesHandler(IUserService userService, IUserContext userContext)
    : IRequestHandler<GetUserByIdQuery, ResponseOf<GetUserByIdResult>>,
        IRequestHandler<GetUserProfileQuery, ResponseOf<GetUserProfileResult>>,
        IRequestHandler<PaginateUsersQuery, PaginationResponse<List<PaginateUsersResult>>>
{
    public async Task<ResponseOf<GetUserByIdResult>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken
    ) => await userService.GetUserByIdAsync(request.Id, cancellationToken);

    public async Task<ResponseOf<GetUserProfileResult>> Handle(
        GetUserProfileQuery request,
        CancellationToken cancellationToken
    ) => await userService.GetUserProfileAsync(userContext.Id.Value, cancellationToken);

    public async Task<PaginationResponse<List<PaginateUsersResult>>> Handle(
        PaginateUsersQuery request,
        CancellationToken cancellationToken
    ) => await userService.PaginateAsync(request, cancellationToken);
}
