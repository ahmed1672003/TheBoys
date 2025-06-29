using TheBoys.Application.Features.News.Commands.Handler.Create;
using TheBoys.Application.Features.News.Commands.Handler.Delete;
using TheBoys.Application.Features.News.Commands.Handler.Update;

namespace TheBoys.Application.Features.News.Commands;

internal sealed class PrtlNewsCommandsHandler(IPrtlNewsService prtlNewsService)
    : IRequestHandler<CreateNewsCommand, Response>,
        IRequestHandler<UpdateNewsCommand, Response>,
        IRequestHandler<DeleteNewsCommand, Response>
{
    public async Task<Response> Handle(
        CreateNewsCommand request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.CreateAsync(request, cancellationToken);

    public async Task<Response> Handle(
        DeleteNewsCommand request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.DeleteAsync(request, cancellationToken);

    public async Task<Response> Handle(
        UpdateNewsCommand request,
        CancellationToken cancellationToken
    ) => await prtlNewsService.UpdateAsync(request, cancellationToken);
}
