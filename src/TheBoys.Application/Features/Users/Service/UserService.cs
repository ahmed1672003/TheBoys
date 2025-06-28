using Microsoft.AspNetCore.Identity;
using TheBoys.Application.Abstractions;
using TheBoys.Application.Features.Users.Commands.Login;
using TheBoys.Domain.Abstractions;
using TheBoys.Domain.Entities.Users;

namespace TheBoys.Application.Features.Users.Service;

internal sealed class UserService(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    IJwtManager jwtManager,
    IUnitOfWork unitOfWork
) : IUserService
{
    public async Task<ResponseOf<LoginUserResult>> LoginAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var user = await userRepository.GetUserForLoginAsync(
            command.UserNameOrEmail,
            cancellationToken
        );
        user.HashedPassword = passwordHasher.HashPassword(user, command.Password);
        var token = await jwtManager.GenerateTokenAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new ResponseOf<LoginUserResult>()
        {
            Success = true,
            Result = new LoginUserResult() { Token = token },
        };
    }
}
