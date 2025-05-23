using TheBoys.Contracts.News;
using TheBoys.Shared.Misc;

namespace TheBoys.API.Dtos;

public class NewsDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsFeatured { get; set; }
    public string NewsImg { get; set; }
    public NewsTranslationDto NewsDetails { get; set; }
    public List<LanguageModel> Languages { get; set; }

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
            dto.Languages = contarct
                .Languages.Select(l => new LanguageModel()
                {
                    Id = l.Id,
                    Name = l.Name,
                    Code = l.Code,
                    Flag = l.Flag
                })
                .ToList();

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
                            Id = lang.Id,
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
