using TheBoys.Application.Features.Users.Commands.ChangeOtherPassword;

namespace TheBoys.Application.Features.Users.Commands;

internal sealed class UserCommandsHandler(IUserService userService, IUserContext userContext)
    : IRequestHandler<AuthUserCommand, ResponseOf<AuthUserResult>>,
        IRequestHandler<UpdateUserCommand, Response>,
        IRequestHandler<AddUserCommand, Response>,
        IRequestHandler<ChangePasswordCommand, Response>,
        IRequestHandler<DeleteUserCommand, Response>,
        IRequestHandler<ChangeOtherPasswordCommand, Response>
{
    public async Task<ResponseOf<AuthUserResult>> Handle(
        AuthUserCommand request,
        CancellationToken cancellationToken
    ) => await userService.LoginAsync(request, cancellationToken);

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
    ) =>
        await userService.ChangePasswordAsync(
            userContext.Id.Value,
            request.NewPassword,
            cancellationToken
        );

    public async Task<Response> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken
    ) => await userService.DeleteAsync(request.Id, cancellationToken);

    public async Task<Response> Handle(
        ChangeOtherPasswordCommand request,
        CancellationToken cancellationToken
    ) => await userService.ChangePasswordAsync(request.Id, request.Password, cancellationToken);
}
