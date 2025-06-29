namespace TheBoys.Application.Features.Users.Commands.AddUser;

public sealed record AddUserCommand(
    string Name,
    string Email,
    string Username,
    string Phone,
    string Password,
    string ConfirmedPassword,
    int RoleId
) : IRequest<Response>;
