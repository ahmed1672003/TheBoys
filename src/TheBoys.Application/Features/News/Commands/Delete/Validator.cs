namespace TheBoys.Application.Features.News.Commands.Delete;

public sealed class DeleteNewsValidator : AbstractValidator<DeleteNewsCommand>
{
    readonly IPrtlNewsRepository _newsRepository;
    readonly IStringLocalizer _stringLocalizer;

    public DeleteNewsValidator(IStringLocalizer stringLocalizer, IPrtlNewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
        _stringLocalizer = stringLocalizer;

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _newsRepository.AnyAsync(x => x.NewsId == req.Id, ct);
                }
            )
            .WithMessage(_stringLocalizer["NewsNotFound"]);
        _newsRepository = newsRepository;
    }
}
