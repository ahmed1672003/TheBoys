using TheBoys.Contracts.News;
using TheBoys.Shared.Misc;

namespace TheBoys.API.Dtos;

public class NewsTranslationDto
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string Head { get; set; }
    public string Abbr { get; set; }
    public string Body { get; set; }
    public string Source { get; set; }
    public string ImgAlt { get; set; }
    public LanguageModel Language { get; set; }

    public static implicit operator NewsTranslationDto(NewsTranslationContract contract) =>
        contract is not null
            ? new NewsTranslationDto()
            {
                Id = contract.Id,
                Abbr = contract.Abbr,
                Body = contract.Body,
                Head = contract.Head,
                ImgAlt = contract.ImgAlt,
                LanguageId = contract.LanguageId,
                Source = contract.Source,
                Language = contract.Language
            }
            : null;
}
