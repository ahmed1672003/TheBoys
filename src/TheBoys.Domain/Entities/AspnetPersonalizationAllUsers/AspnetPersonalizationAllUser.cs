using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheBoys.Domain.Entities.AspnetPathes;

namespace TheBoys.Domain.Entities.AspnetPersonalizationAllUsers;

[Table("aspnet_PersonalizationAllUsers")]
public partial class AspnetPersonalizationAllUser
{
    [Key]
    public Guid PathId { get; set; }

    [Required]
    [Column(TypeName = "image")]
    public byte[] PageSettings { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [ForeignKey("PathId")]
    [InverseProperty("AspnetPersonalizationAllUser")]
    public virtual AspnetPath Path { get; set; }
}
