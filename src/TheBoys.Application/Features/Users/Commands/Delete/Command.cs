namespace TheBoys.Application.Features.Users.Commands.Delete;

public sealed record DeleteUserCommand(int Id) : IRequest<Response>;
