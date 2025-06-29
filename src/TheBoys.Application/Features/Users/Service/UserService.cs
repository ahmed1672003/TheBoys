using Microsoft.AspNetCore.Identity;
using TheBoys.Application.Abstractions;
using TheBoys.Application.Features.Users.Queries.GetById;
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
    public async Task<ResponseOf<AuthUserResult>> LoginAsync(
        AuthUserCommand command,
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
        return new ResponseOf<AuthUserResult>()
        {
            Success = true,
            Result = new AuthUserResult() { Token = token },
        };
    }

    public async Task<Response> AddUserAsync(
        AddUserCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var user = new User
        {
            Name = command.Name,
            Phone = command.Phone,
            Email = command.Email,
            Username = command.Username,
            RoleId = command.RoleId,
        };
        user.HashedPassword = passwordHasher.HashPassword(user, command.Password);
        await userRepository.CreateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new Response() { Success = true };
    }

    public async Task<ResponseOf<GetUserByIdResult>> GetUserByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        var user = await userRepository.GetUserById(id, cancellationToken);
        return new ResponseOf<GetUserByIdResult>()
        {
            Success = true,
            Result = new GetUserByIdResult()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                Username = user.Username,
                Role = new GetUserByIdResult.RoleResult()
                {
                    Id = user.RoleId,
                    Type = user.Role.Type,
                },
            },
        };
    }

    public async Task<ResponseOf<GetUserProfileResult>> GetUserProfileAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        var user = await userRepository.GetUserById(id, cancellationToken);
        return new ResponseOf<GetUserProfileResult>()
        {
            Success = true,
            Result = new GetUserProfileResult()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                Username = user.Username,
                Role = new GetUserProfileResult.RoleResult()
                {
                    Id = user.RoleId,
                    Type = user.Role.Type,
                },
            },
        };
    }

    public async Task<Response> UpdateAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var user = await userRepository.GetUserByIdForUpdateAsync(command.Id, cancellationToken);
        user.Username = command.Username;
        user.Phone = command.Phone;
        user.Name = command.Name;
        user.Email = command.Email;
        user.RoleId = command.RoleId;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new() { Success = true };
    }

    public async Task<Response> ChangePasswordAsync(
        ChangePasswordCommand command,
        int userId,
        CancellationToken cancellationToken = default
    )
    {
        var user = await userRepository.GetUserByIdForUpdateAsync(userId, cancellationToken);
        user.HashedPassword = passwordHasher.HashPassword(user, command.NewPassword);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new Response() { Success = true };
    }

    public async Task<Response> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetUserByIdForDeleteAsync(id, cancellationToken);
        await userRepository.DeleteAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new Response() { Success = true };
    }

    //public Task<PaginationResponse<List<PaginateUsersResult>>> PaginateAsync(
    //    CancellationToken cancellationToken = default
    //)
    //{
    //    var users = userRepository
    //}
}
