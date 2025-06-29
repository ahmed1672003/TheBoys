namespace TheBoys.Application.Features.Users.Commands.ChangePassword;

public sealed record ChangePasswordCommand(
    string OldPassword,
    string NewPassword,
    string ConfirmedNewPassword
) : IRequest<Response>;
