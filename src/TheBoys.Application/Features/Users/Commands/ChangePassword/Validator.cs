using Microsoft.AspNetCore.Identity;
using TheBoys.Domain.Entities.Users;
using TheBoys.Shared.Abstractions;
using TheBoys.Shared.Extensions;

namespace TheBoys.Application.Features.Users.Commands.ChangePassword;

public sealed class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    readonly IUserContext _userContext;
    readonly IUserRepository _userRepository;
    readonly IStringLocalizer _stringLocalizer;
    readonly IPasswordHasher<User> _passwordHasher;

    public ChangePasswordValidator(
        IUserContext userContext,
        IUserRepository userRepository,
        IStringLocalizer stringLocalizer,
        IPasswordHasher<User> passwordHasher
    )
    {
        _userContext = userContext;
        _userRepository = userRepository;
        _stringLocalizer = stringLocalizer;
        _passwordHasher = passwordHasher;

        RuleFor(x => x.OldPassword)
            .NotNull()
            .WithMessage(_stringLocalizer["OldPasswordRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["OldPasswordRequired"])
            .Must(x => x.IsValidPassword())
            .WithMessage(_stringLocalizer["OldPasswordNotValid"]);

        RuleFor(x => x.NewPassword)
            .NotNull()
            .WithMessage(_stringLocalizer["PasswordRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["PasswordRequired"])
            .Equal(x => x.ConfirmedNewPassword)
            .WithMessage(_stringLocalizer["PasswordMustEqualConfirmedNewPassword"])
            .Must(x => x.IsValidPassword())
            .WithMessage(_stringLocalizer["PasswordNotValid"]);

        RuleFor(x => x.ConfirmedNewPassword)
            .NotNull()
            .WithMessage(_stringLocalizer["ConfirmedPasswordRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["ConfirmedPasswordRequired"])
            .Equal(x => x.NewPassword)
            .WithMessage(_stringLocalizer["PasswordMustEqualConfirmedNewPassword"])
            .Must(x => x.IsValidPassword())
            .WithMessage(_stringLocalizer["PasswordNotValid"]);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var user = await userRepository.GetUserForValidatePasswordAsync(
                        _userContext.Id.Value,
                        ct
                    );

                    var result = _passwordHasher.VerifyHashedPassword(
                        user,
                        user.HashedPassword,
                        req.OldPassword
                    );
                    return result == PasswordVerificationResult.Success
                        || result == PasswordVerificationResult.SuccessRehashNeeded;
                }
            )
            .WithMessage(_stringLocalizer["IncorrectPassword"]);
    }
}
