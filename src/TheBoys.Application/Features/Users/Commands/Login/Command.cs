namespace TheBoys.Application.Features.Users.Commands.Login;

public sealed record LoginUserCommand(string UserNameOrEmail, string Password)
    : IRequest<ResponseOf<LoginUserResult>>;
