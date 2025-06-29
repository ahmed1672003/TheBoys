using TheBoys.Domain.Entities.Users;
using TheBoys.Shared.Extensions;

namespace TheBoys.Application.Features.Users.Commands.ChangeOtherPassword;

public sealed class ChangeOtherPasswordValidator : AbstractValidator<ChangeOtherPasswordCommand>
{
    readonly IUserContext _userContext;
    readonly IUserRepository _userRepository;
    readonly IStringLocalizer _stringLocalizer;

    public ChangeOtherPasswordValidator(
        IUserContext userContext,
        IUserRepository userRepository,
        IStringLocalizer stringLocalizer
    )
    {
        _userContext = userContext;
        _userRepository = userRepository;
        _stringLocalizer = stringLocalizer;

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage(_stringLocalizer["PasswordRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["PasswordRequired"])
            .Must(x => x.IsValidPassword())
            .WithMessage(_stringLocalizer["PasswordNotValid"]);

        RuleFor(x => x.ConfirmedPassword)
            .NotNull()
            .WithMessage(_stringLocalizer["ConfirmedPasswordRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["ConfirmedPasswordRequired"])
            .Equal(x => x.ConfirmedPassword)
            .WithMessage(_stringLocalizer["PasswordMustEqualConfirmedNewPassword"])
            .Must(x => x.IsValidPassword())
            .WithMessage(_stringLocalizer["PasswordNotValid"]);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _userRepository.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(_stringLocalizer["UserNotExist"]);
    }
}
