using TheBoys.Shared.Enums.Roles;
using static TheBoys.Contracts.Users.UsersPaginationContract;

namespace TheBoys.Application.Features.Users.Queries.Paginate;

public sealed record PaginateUsersResult
{
    public PaginateUsersResult(UserContract userContract)
    {
        Id = userContract.Id;
        Name = userContract.Name;
        Email = userContract.Email;
        Phone = userContract.Phone;
        Username = userContract.Username;
        Role = new RoleResult() { Id = userContract.Role.Id, Type = userContract.Role.Type };
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public RoleResult Role { get; set; }

    public sealed record RoleResult
    {
        public int Id { get; set; }

        public RoleType Type { get; set; }
    }
}
