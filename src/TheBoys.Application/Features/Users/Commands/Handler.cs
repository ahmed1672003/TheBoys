using TheBoys.Application.Features.Users.Commands.Login;
using TheBoys.Application.Features.Users.Service;

namespace TheBoys.Application.Features.Users.Commands;

internal sealed class UserCommandsHandler(IUserService userService)
    : IRequestHandler<LoginUserCommand, ResponseOf<LoginUserResult>>
{
    public Task<ResponseOf<LoginUserResult>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken
    ) => userService.LoginAsync(request, cancellationToken);
}
