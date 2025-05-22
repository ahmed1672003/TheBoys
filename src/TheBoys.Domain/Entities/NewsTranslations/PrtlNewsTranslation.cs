using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TheBoys.Domain.Entities.Languages;
using TheBoys.Domain.Entities.News;

namespace TheBoys.Domain.Entities.NewsTranslations;

/// <summary>
/// جدول ترجمة الاخبار لاكثر من لغة
/// </summary>
[Table("prtl_News_Translations")]
[Index("NewsId", "LangId", Name = "IX_prtl_News_Translations_NewsId_LangId_Includes")]
public partial class PrtlNewsTranslation
{
    /// <summary>
    /// معرف عام
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// عنوان الاخبار
    /// </summary>
    [Required]
    [Column("News_Head")]
    [StringLength(500)]
    public string NewsHead { get; set; }

    /// <summary>
    /// مختصر الخبر
    /// </summary>
    [Required]
    [Column("News_Abbr")]
    public string NewsAbbr { get; set; }

    /// <summary>
    /// تفاصيل الخير
    /// </summary>
    [Required]
    [Column("News_Body")]
    public string NewsBody { get; set; }

    /// <summary>
    /// مصدر الخبر
    /// </summary>
    [Required]
    [Column("News_Source")]
    public string NewsSource { get; set; }

    /// <summary>
    /// معرف اللغة
    /// </summary>
    [Column("Lang_Id")]
    public int LangId { get; set; }

    /// <summary>
    /// معرف الخبر
    /// </summary>
    [Column("News_Id")]
    public int NewsId { get; set; }

    [Column("Img_alt")]
    [StringLength(500)]
    public string ImgAlt { get; set; }

    [ForeignKey("LangId")]
    [InverseProperty("PrtlNewsTranslations")]
    public virtual PrtlLanguage Lang { get; set; }

    [ForeignKey("NewsId")]
    [InverseProperty("PrtlNewsTranslations")]
    public virtual PrtlNews News { get; set; }
}
