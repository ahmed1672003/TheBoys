using System.ComponentModel.DataAnnotations.Schema;

namespace TheBoys.API.Entities;

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

    public ICollection<NewsImage> NewsImages { get; set; } = new List<NewsImage>();
    public ICollection<NewsTranslation> NewsTranslations { get; set; } =
        new List<NewsTranslation>();
}
