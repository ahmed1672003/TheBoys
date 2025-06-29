namespace TheBoys.Application.Features.Users.Commands.ChangeOtherPassword;

public sealed record ChangeOtherPasswordCommand(int Id, string Password, string ConfirmedPassword)
    : IRequest<Response>;
