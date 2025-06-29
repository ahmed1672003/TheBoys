using TheBoys.Application.Features.Users.Queries.GetById;
using TheBoys.Application.Features.Users.Queries.GetProfile;
using TheBoys.Application.Features.Users.Service;
using TheBoys.Shared.Abstractions;

namespace TheBoys.Application.Features.Users.Queries;

internal sealed class UserQueriesHandler(IUserService userService, IUserContext userContext)
    : IRequestHandler<GetUserByIdQuery, ResponseOf<GetUserByIdResult>>,
        IRequestHandler<GetUserProfileQuery, ResponseOf<GetUserProfileResult>>
{
    public async Task<ResponseOf<GetUserByIdResult>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken
    ) => await userService.GetUserByIdAsync(request.Id, cancellationToken);

    public async Task<ResponseOf<GetUserProfileResult>> Handle(
        GetUserProfileQuery request,
        CancellationToken cancellationToken
    ) => await userService.GetUserProfileAsync(userContext.Id.Value, cancellationToken);
}
