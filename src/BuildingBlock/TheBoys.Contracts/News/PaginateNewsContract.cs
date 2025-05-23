namespace TheBoys.Contracts.News;

public record PaginateNewsContract : PaginateContract
{
    public int LanguageId { get; set; }
}
