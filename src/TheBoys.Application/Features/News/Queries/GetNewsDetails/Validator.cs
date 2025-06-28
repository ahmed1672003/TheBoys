namespace TheBoys.Application.Features.News.Queries.GetNewsDetails;

public sealed class GetNewsDetailsValidator : AbstractValidator<GetNewsDetailsQuery>
{
    readonly IStringLocalizer _stringLocalizer;
    readonly IPrtlNewsRepository _rtlNewsRepository;

    public GetNewsDetailsValidator(
        IStringLocalizer stringLocalizer,
        IPrtlNewsRepository rtlNewsRepository
    )
    {
        _stringLocalizer = stringLocalizer;
        _rtlNewsRepository = rtlNewsRepository;

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _rtlNewsRepository.AnyAsync(x => x.NewsId == req.Id);
                }
            )
            .WithMessage(_stringLocalizer["newsNotFound"]);
    }
}
