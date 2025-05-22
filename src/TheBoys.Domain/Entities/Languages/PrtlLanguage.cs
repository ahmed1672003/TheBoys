using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheBoys.Domain.Entities.NewsTranslations;

namespace TheBoys.Domain.Entities.Languages;

/// <summary>
/// يحتوى على اللغات المعتمدة فى كل تفاصيل قاعدة البيانات
/// </summary>
[Table("prtl_Languages")]
public partial class PrtlLanguage
{
    /// <summary>
    /// معرف  اللغة المستخدمة
    /// </summary>
    [Key]
    [Column("Lang_Id")]
    public int LangId { get; set; }

    /// <summary>
    /// كود اللهجة المحلية
    /// </summary>
    [Required]
    [Column("LCID")]
    [StringLength(5)]
    public string Lcid { get; set; }

    [InverseProperty("Lang")]
    public virtual ICollection<PrtlNewsTranslation> PrtlNewsTranslations { get; set; } =
        new List<PrtlNewsTranslation>();
}
