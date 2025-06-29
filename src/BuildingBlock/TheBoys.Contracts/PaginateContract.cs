namespace TheBoys.Contracts;

public record PaginateContract
{
    public virtual int PageIndex { get; set; }
    public virtual int PageSize { get; set; }
    public string? Search { get; set; } = string.Empty;
}
