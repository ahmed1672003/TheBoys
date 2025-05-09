namespace TheBoys.API.Dtos;

public class NewsDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsFeature { get; set; }
    public string NewsImg { get; set; }
    public NewsTranslationDto NewsDetails { get; set; }
    public List<NewsImageDto> Images { get; set; } = new();
}
