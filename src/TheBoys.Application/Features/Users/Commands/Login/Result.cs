namespace TheBoys.Application.Features.Users.Commands.Login;

public sealed record LoginUserResult
{
    public string Token { get; set; }
}
