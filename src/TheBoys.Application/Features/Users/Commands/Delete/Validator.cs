using TheBoys.Domain.Entities.Users;

namespace TheBoys.Application.Features.Users.Commands.Delete;

public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    readonly IStringLocalizer _stringLocalizer;
    readonly IUserRepository _userRepository;

    public DeleteUserValidator(IStringLocalizer stringLocalizer, IUserRepository userRepository)
    {
        _stringLocalizer = stringLocalizer;
        _userRepository = userRepository;

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _userRepository.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(_stringLocalizer["userNotExist"]);
    }
}
