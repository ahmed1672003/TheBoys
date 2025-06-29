using TheBoys.Application.Features.Users.Commands.AddUser;
using TheBoys.Application.Features.Users.Commands.ChangePassword;
using TheBoys.Application.Features.Users.Commands.Login;
using TheBoys.Application.Features.Users.Commands.Update;
using TheBoys.Application.Features.Users.Service;
using TheBoys.Shared.Abstractions;

namespace TheBoys.Application.Features.Users.Commands;

internal sealed class UserCommandsHandler(IUserService userService, IUserContext userContext)
    : IRequestHandler<AuthUserCommand, ResponseOf<AuthUserResult>>,
        IRequestHandler<UpdateUserCommand, Response>,
        IRequestHandler<AddUserCommand, Response>,
        IRequestHandler<ChangePasswordCommand, Response>
{
    public Task<ResponseOf<AuthUserResult>> Handle(
        AuthUserCommand request,
        CancellationToken cancellationToken
    ) => userService.LoginAsync(request, cancellationToken);

    public async Task<Response> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken
    ) => await userService.UpdateAsync(request, cancellationToken);

    public async Task<Response> Handle(
        AddUserCommand request,
        CancellationToken cancellationToken
    ) => await userService.AddUserAsync(request, cancellationToken);

    public async Task<Response> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken
    ) => await userService.ChangePasswordAsync(request, userContext.Id.Value, cancellationToken);
}
