using TheBoys.Contracts.News;
using TheBoys.Shared.Misc;

namespace TheBoys.Application.Features.News.Dtos;

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

    public static implicit operator NewsTranslationDto(NewsTranslationContract contract)
    {
        var dto = new NewsTranslationDto()
        {
            Id = contract.Id,
            Abbr = contract.Abbr,
            Body = contract.Body,
            Head = contract.Head,
            ImgAlt = contract.ImgAlt,
            LanguageId = contract.LanguageId,
            Source = contract.Source,
            Language = contract.Language
        };

        var language = StaticLanguages.languageModels.FirstOrDefault(x =>
            x.Code.ToLower() == contract.Language?.Code.ToLower()
        );
        if (language is not null)
        {
            dto.Language.Name = language.Name;
            dto.Language.Flag = language.Flag;
        }

        return dto;
    }
}
