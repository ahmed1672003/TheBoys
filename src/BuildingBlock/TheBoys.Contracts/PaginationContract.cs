namespace TheBoys.Contracts;

public record PaginationContract<TEntity>
{
    public List<TEntity> Elements { get; set; } = new();

    public int TotalCount { get; set; }
}
