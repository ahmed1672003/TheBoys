namespace TheBoys.Application.Features.Users.Commands.Update;

public sealed record UpdateUserCommand(
    int Id,
    string Name,
    string Username,
    string Email,
    string Phone,
    int RoleId
) : IRequest<Response>;
