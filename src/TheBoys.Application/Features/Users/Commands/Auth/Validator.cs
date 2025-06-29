using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TheBoys.Domain.Entities.Users;

namespace TheBoys.Application.Features.Users.Commands.Login;

public sealed class LoginUserValidator : AbstractValidator<AuthUserCommand>
{
    readonly IStringLocalizer _stringLocalizer;
    readonly IUserRepository _userRepository;
    readonly IPasswordHasher<User> _passwordHasher;

    public LoginUserValidator(
        IStringLocalizer stringLocalizer,
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher
    )
    {
        _stringLocalizer = stringLocalizer;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;

        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x)
            .MustAsync(
                (req, ct) =>
                {
                    if (new EmailAddressAttribute().IsValid(req.UserNameOrEmail))
                    {
                        return _userRepository.AnyAsync(x =>
                            x.Email.ToLower() == req.UserNameOrEmail.ToLower()
                        );
                    }
                    else
                    {
                        return _userRepository.AnyAsync(x =>
                            x.Username.ToLower() == req.UserNameOrEmail.ToLower()
                        );
                    }
                }
            )
            .WithMessage(_stringLocalizer["usernameOrEmailNotExist"]);
        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var user = await userRepository.GetUserForValidatePasswordAsync(
                        req.UserNameOrEmail,
                        ct
                    );
                    var result = _passwordHasher.VerifyHashedPassword(
                        user,
                        user.HashedPassword,
                        req.Password
                    );
                    return result == PasswordVerificationResult.Success
                        || result == PasswordVerificationResult.SuccessRehashNeeded;
                }
            )
            .WithMessage(_stringLocalizer["IncorrectPassword"]);
    }
}
