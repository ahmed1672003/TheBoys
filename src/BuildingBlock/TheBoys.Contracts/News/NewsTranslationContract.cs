using TheBoys.Shared.Misc;

namespace TheBoys.Contracts.News;

public class NewsTranslationContract
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string Head { get; set; }
    public string Abbr { get; set; }
    public string Body { get; set; }
    public string Source { get; set; }
    public string ImgAlt { get; set; }
    public LanguageModel Language { get; set; }
}
