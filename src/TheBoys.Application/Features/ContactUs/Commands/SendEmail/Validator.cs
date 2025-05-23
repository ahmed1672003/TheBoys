namespace TheBoys.Application.Features.ContactUs.Commands.SendEmail;

public sealed class SendEmailValidator : AbstractValidator<SendEmailCommand>
{
    readonly IStringLocalizer _stringLocalizer;

    public SendEmailValidator(IStringLocalizer stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage(_stringLocalizer["EmailRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["EmailRequired"])
            .EmailAddress()
            .WithMessage(_stringLocalizer["EmailNotValid"]);
    }
}
