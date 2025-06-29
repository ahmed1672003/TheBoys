namespace TheBoys.Application.Features.Users.Commands.Auth;

public sealed record AuthUserCommand(string UserNameOrEmail, string Password)
    : IRequest<ResponseOf<AuthUserResult>>;
