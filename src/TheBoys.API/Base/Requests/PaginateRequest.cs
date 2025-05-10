namespace TheBoys.API.Base.Requests;

public class PaginateRequest
{
    public int LanguageId { get; set; } = 2;
    public virtual int PageIndex { get; set; } = 1;
    public virtual int PageSize { get; set; } = 10;
    public string? Search { get; set; } = string.Empty;
}
