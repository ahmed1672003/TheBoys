namespace TheBoys.Contracts.Users;

public sealed record PaginateUsersContract : PaginateContract
{
    public int? RoleId { get; set; }
}
