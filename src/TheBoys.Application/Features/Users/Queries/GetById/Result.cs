using TheBoys.Shared.Enums.Roles;

namespace TheBoys.Application.Features.Users.Queries.GetById;

public sealed record GetUserByIdResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public RoleResult Role { get; set; }

    public record RoleResult
    {
        public int Id { get; set; }
        public string Name => Type.ToString();
        public RoleType Type { get; set; }
    }
}
