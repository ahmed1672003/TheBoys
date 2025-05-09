using System.ComponentModel.DataAnnotations.Schema;

namespace TheBoys.API.Entities;

public class NewsImage
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("NewsId")]
    public int NewsId { get; set; }

    [Column("NewsUrl")]
    public string NewsUrl { get; set; }

    public News News { get; set; }
}

public class News
{
    [Column("News_Id")]
    public int NewsId { get; set; }

    [Column("News_date")]
    public DateTime NewsDate { get; set; }

    [Column("News_img")]
    public string NewsImg { get; set; }

    [Column("Owner_ID")]
    public Guid OwnerId { get; set; }

    [Column("IsFeature")]
    public bool IsFeature { get; set; }

    public ICollection<NewsImage> NewsImages { get; set; }
    public ICollection<NewsTranslation> NewsTranslations { get; set; }
}

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

    [Column("News_Id")]
    public int? NewsId { get; set; }

    // Navigation Property
    public News News { get; set; }
}
