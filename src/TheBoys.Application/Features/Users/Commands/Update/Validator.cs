using TheBoys.Domain.Entities.Roles;
using TheBoys.Domain.Entities.Users;
using TheBoys.Shared.Extensions;

namespace TheBoys.Application.Features.Users.Commands.Update;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    readonly IStringLocalizer _stringLocalizer;
    readonly IRoleRepository _roleRepository;
    readonly IUserRepository _userRepository;

    public UpdateUserValidator(
        IStringLocalizer stringLocalizer,
        IUserRepository userRepository,
        IRoleRepository roleRepository
    )
    {
        _stringLocalizer = stringLocalizer;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage(_stringLocalizer["NameRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["NameRequired"]);

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage(_stringLocalizer["EmailRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["EmailRequired"])
            .EmailAddress()
            .WithMessage(_stringLocalizer["EmailAddressNotValid"]);

        RuleFor(x => x.Username)
            .NotNull()
            .WithMessage(_stringLocalizer["UsernameRequired"])
            .NotEmpty()
            .WithMessage(_stringLocalizer["UsernameRequired"]);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _userRepository.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(_stringLocalizer["UserNotExist"]);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await _userRepository.AnyAsync(x =>
                        x.Id != req.Id && x.Username.ToLower() == req.Username.ToLower()
                    );
                }
            )
            .WithMessage(_stringLocalizer["UsernameExist"]);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await _userRepository.AnyAsync(x =>
                        x.Id != req.Id && x.Email.ToLower() == req.Email.ToLower()
                    );
                }
            )
            .WithMessage(_stringLocalizer["EmailExist"]);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await _userRepository.AnyAsync(x =>
                        x.Id != req.Id && x.Phone == req.Phone
                    );
                }
            )
            .When(x => x.Phone.HasValue())
            .WithMessage(_stringLocalizer["PhoneExist"]);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _roleRepository.AnyAsync(x => x.Id == req.RoleId);
                }
            )
            .WithMessage(_stringLocalizer["RoleNotExist"]);
    }
}
