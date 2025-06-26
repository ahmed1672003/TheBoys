namespace TheBoys.Domain.Entities.Tokens;

public sealed class Token
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string JWT { get; set; }
    public int MyProperty { get; set; }
}
