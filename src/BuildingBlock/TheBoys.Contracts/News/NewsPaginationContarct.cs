using TheBoys.Shared.Misc;
using static TheBoys.Contracts.News.NewsPaginationContarct;

namespace TheBoys.Contracts.News;

public record NewsPaginationContarct : PaginationContract<NewsContract>
{
    public record NewsContract
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsFeatured { get; set; }
        public string NewsImg { get; set; }
        public NewsTranslationContract NewsDetails { get; set; }
        public List<LanguageModel> Languages { get; set; }
    }
}
