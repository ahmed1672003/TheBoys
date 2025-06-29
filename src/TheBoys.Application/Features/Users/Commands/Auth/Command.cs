namespace TheBoys.Application.Features.Users.Commands.Login;

public sealed record AuthUserCommand(string UserNameOrEmail, string Password)
    : IRequest<ResponseOf<AuthUserResult>>;
