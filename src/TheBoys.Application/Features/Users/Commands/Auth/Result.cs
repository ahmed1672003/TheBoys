namespace TheBoys.Application.Features.Users.Commands.Auth;

public sealed record AuthUserResult
{
    public string Token { get; set; }
}
