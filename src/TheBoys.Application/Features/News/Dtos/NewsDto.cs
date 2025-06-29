using TheBoys.Contracts.News;

namespace TheBoys.Application.Features.News.Dtos;

public class NewsDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsFeatured { get; set; }
    public string NewsImg { get; set; }
    public NewsTranslationDto NewsDetails { get; set; }
    public List<LanguageModel> Languages { get; set; }
    public bool IsEvent { get; set; }

    public static implicit operator NewsDto(NewsPaginationContarct.NewsContract contarct)
    {
        var dto = new NewsDto()
        {
            Id = contarct.Id,
            Date = contarct.Date,
            IsFeatured = contarct.IsFeatured,
            NewsImg = contarct.NewsImg,
            Languages = new(),
            NewsDetails = contarct.NewsDetails
        };

        if (contarct.Languages is not null && contarct.Languages.Any())
        {
            foreach (var language in contarct.Languages)
            {
                var lang = StaticLanguages.languageModels.FirstOrDefault(x =>
                    x.Code == language.Code
                );

                if (lang is not null)
                {
                    dto.Languages.Add(
                        new LanguageModel()
                        {
                            Id = language.Id,
                            Code = lang.Code,
                            Flag = lang.Flag,
                            Name = lang.Name
                        }
                    );
                }
            }
        }

        return dto;
    }
}
