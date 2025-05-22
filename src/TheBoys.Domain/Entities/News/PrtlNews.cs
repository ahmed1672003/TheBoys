using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TheBoys.Domain.Entities.NewsTranslations;

namespace TheBoys.Domain.Entities.News;

/// <summary>
/// يحتوي على جميع المعلومات الخاصة بالاخبار
///
/// </summary>
[Table("prtl_news")]
[Index(
    "Published",
    "NewsDate",
    Name = "IX_prtl_news_Published_NewsDate_Includes",
    IsDescending = new[] { false, true }
)]
public partial class PrtlNews
{
    /// <summary>
    /// معرف الخبر
    /// </summary>
    [Key]
    [Column("News_Id")]
    public int NewsId { get; set; }

    [Column("News_date", TypeName = "datetime")]
    public DateTime NewsDate { get; set; }

    [Column("News_img")]
    [StringLength(50)]
    public string NewsImg { get; set; }

    [Column("Owner_ID")]
    public Guid? OwnerId { get; set; }

    public bool Published { get; set; }

    [Column("currentNews_date", TypeName = "datetime")]
    public DateTime? CurrentNewsDate { get; set; }

    public bool IsFeatured { get; set; }

    [InverseProperty("News")]
    public virtual ICollection<PrtlNewsTranslation> PrtlNewsTranslations { get; set; } =
        new List<PrtlNewsTranslation>();
}
