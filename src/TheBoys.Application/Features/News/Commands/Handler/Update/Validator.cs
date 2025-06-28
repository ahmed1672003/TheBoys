namespace TheBoys.Application.Features.News.Commands.Handler.Update;

public sealed class UpdateNewsValidator : AbstractValidator<UpdateNewsCommand>
{
    readonly IPrtlNewsRepository _newsRepository;
    readonly IPrtlNewsTranslationRepository _prtlNewsTranslationRepository;
    readonly IStringLocalizer _stringLocalizer;

    public UpdateNewsValidator(
        IPrtlNewsTranslationRepository prtlNewsTranslationRepository,
        IPrtlNewsRepository newsRepository,
        IStringLocalizer stringLocalizer
    )
    {
        _prtlNewsTranslationRepository = prtlNewsTranslationRepository;
        _newsRepository = newsRepository;
        _stringLocalizer = stringLocalizer;

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _newsRepository.AnyAsync(x => x.NewsId == req.Id, ct);
                }
            )
            .WithMessage(_stringLocalizer["newsNotFound"]);

        RuleForEach(x => x.Translations.Where(x => x.Id.HasValue))
            .MustAsync(
                async (req, ct) =>
                {
                    return await _prtlNewsTranslationRepository.AnyAsync(x => x.Id == req.Id, ct);
                }
            )
            .OverridePropertyName(nameof(UpdateNewsCommand.Translations))
            .WithMessage(_stringLocalizer["newsTranslationNotFound"]);
    }
}
