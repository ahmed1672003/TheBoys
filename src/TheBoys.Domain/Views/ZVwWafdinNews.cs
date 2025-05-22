using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheBoys.Domain.Views;

[Keyless]
public partial class ZVwWafdinNews
{
    [Column("News_Id")]
    public int NewsId { get; set; }

    [Column("News_date", TypeName = "datetime")]
    public DateTime NewsDate { get; set; }

    [Column("News_img")]
    [StringLength(50)]
    public string NewsImg { get; set; }

    [Column("Owner_ID")]
    public Guid? OwnerId { get; set; }
}
