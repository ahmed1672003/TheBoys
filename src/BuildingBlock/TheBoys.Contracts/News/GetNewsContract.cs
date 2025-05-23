namespace TheBoys.Contracts.News;

public sealed record GetNewsContract
{
    public int NewsId { get; set; }
    public int LanguageId { get; set; }
}
