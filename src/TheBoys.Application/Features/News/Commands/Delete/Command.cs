namespace TheBoys.Application.Features.News.Commands.Delete;

public sealed record DeleteNewsCommand(int Id) : IRequest<Response>;
