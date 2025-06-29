using TheBoys.Shared.Enums.Roles;
using static TheBoys.Contracts.Users.UsersPaginationContract;

namespace TheBoys.Contracts.Users;

public sealed record UsersPaginationContract : PaginationContract<UserContract>
{
    public sealed record UserContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public RoleContract Role { get; set; }

        public record RoleContract
        {
            public int Id { get; set; }
            public RoleType Type { get; set; }
            public string Name => Type.ToString();
        }
    }
}
