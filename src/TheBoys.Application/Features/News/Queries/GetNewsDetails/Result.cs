namespace TheBoys.Application.Features.News.Queries.GetNewsDetails;

public sealed record GetNewsDetailsResult
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Guid? OwnerId { get; set; }
    public bool Published { get; set; }
    public bool IsFeatured { get; set; }
    public string NewsImg { get; set; }
    public List<NewsTranslationDto> Translations { get; set; }
}
