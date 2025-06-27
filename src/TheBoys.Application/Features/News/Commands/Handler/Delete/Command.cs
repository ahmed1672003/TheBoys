namespace TheBoys.Application.Features.News.Commands.Handler.Delete;

public sealed record DeleteNewsCommand(int Id) : IRequest<Response>;
