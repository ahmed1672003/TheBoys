using System.ComponentModel.DataAnnotations.Schema;

namespace TheBoys.API.Entities;

public class NewsTranslation
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("News_Head")]
    public string NewsHead { get; set; }

    [Column("News_Abbr")]
    public string NewsAbbr { get; set; }

    [Column("News_Body")]
    public string NewsBody { get; set; }

    [Column("News_Source")]
    public string NewsSource { get; set; }

    [Column("Lang_Id")]
    public int LangId { get; set; }

    [Column("Img_alt")]
    public string ImgAlt { get; set; }

    [Column("NewsId")]
    public int? NewsId { get; set; }

    // Navigation Property
    public News News { get; set; }
}
