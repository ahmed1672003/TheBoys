namespace TheBoys.Application.Features.Users.Commands.Login;

public sealed record AuthUserResult
{
    public string Token { get; set; }
}
