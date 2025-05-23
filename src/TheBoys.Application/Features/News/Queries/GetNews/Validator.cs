namespace TheBoys.Application.Features.News.Queries.GetNews;

public sealed class GetNewsValidator : AbstractValidator<GetNewsQuery>
{
    readonly IPrtlNewsRepository _prtlNewsRepository;
    readonly IPrtlNewsTranslationRepository _prtlNewsTranslationRepository;
    readonly IStringLocalizer _stringLocalizer;

    public GetNewsValidator(
        IPrtlNewsRepository prtlNewsRepository,
        IStringLocalizer stringLocalizer,
        IPrtlNewsTranslationRepository prtlNewsTranslationRepository
    )
    {
        _prtlNewsRepository = prtlNewsRepository;
        _prtlNewsTranslationRepository = prtlNewsTranslationRepository;
        _stringLocalizer = stringLocalizer;
        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _prtlNewsTranslationRepository.AnyAsync(
                        x => x.NewsId == req.Id && x.LangId == req.LanguageId,
                        ct
                    );
                }
            )
            .WithMessage(_stringLocalizer["NewsNotFoundWithThisLanguage"]);
    }
}
