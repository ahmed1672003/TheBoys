using TheBoys.Domain.Entities.Roles;
using TheBoys.Domain.Entities.Users;

namespace TheBoys.Application.Features.Users.Queries.GetById;

public sealed class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    readonly IStringLocalizer _stringLocalizer;
    readonly IUserRepository _userRepository;

    public GetUserByIdValidator(
        IStringLocalizer stringLocalizer,
        IUserRepository userRepository,
        IRoleRepository roleRepository
    )
    {
        _stringLocalizer = stringLocalizer;
        _userRepository = userRepository;
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _userRepository.AnyAsync(x => x.Id == req.Id, ct);
                }
            )
            .WithMessage(_stringLocalizer["UserNotExist"]);
    }
}
