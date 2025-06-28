using TheBoys.Domain.Entities.Languages;

namespace TheBoys.Application.Features.News.Commands.Handler.Create;

public sealed class CreateNewsValidator : AbstractValidator<CreateNewsCommand>
{
    readonly IPrtlLanguageRepository _prtlLanguageRepository;
    readonly IStringLocalizer _stringLocalizer;

    public CreateNewsValidator(
        IStringLocalizer stringLocalizer,
        IPrtlLanguageRepository prtlLanguageRepository
    )
    {
        _prtlLanguageRepository = prtlLanguageRepository;
        _stringLocalizer = stringLocalizer;

        RuleFor(x => x.Translations)
            .Must(x => x != null && x.Count >= 1)
            .WithMessage(stringLocalizer["MustAddOneTranslationAtLeast"]);

        RuleFor(x => x.Translations)
            .Must(x =>
                x.Select(x => x.LangId).ToHashSet().Count() == x.Select(x => x.LangId).Count()
            )
            .WithMessage(_stringLocalizer["Can'tDuplicateLanguageForTranslation"]);

        RuleForEach(x => x.Translations)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _prtlLanguageRepository.AnyAsync(x => x.LangId == req.LangId, ct);
                }
            )
            .WithMessage(_stringLocalizer["LanguageNotFound"]);
    }
}
