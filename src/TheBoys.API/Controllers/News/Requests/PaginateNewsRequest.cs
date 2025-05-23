using TheBoys.Shared.Base.Requests;

namespace TheBoys.API.Controllers.News.Requests;

public record PaginateNewsRequest : PaginateRequest
{
    public int LanguageId { get; set; } = 2;
}
