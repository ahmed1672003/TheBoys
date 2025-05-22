using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheBoys.Domain.Entities.AspnetPathes;
using TheBoys.Domain.Entities.AspnetUsers;

namespace TheBoys.Domain.Entities.AspnetPersonalizationPerUsers;

[Table("aspnet_PersonalizationPerUser")]
public partial class AspnetPersonalizationPerUser
{
    [Key]
    public Guid Id { get; set; }

    public Guid? PathId { get; set; }

    public Guid? UserId { get; set; }

    [Required]
    [Column(TypeName = "image")]
    public byte[] PageSettings { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [ForeignKey("PathId")]
    [InverseProperty("AspnetPersonalizationPerUsers")]
    public virtual AspnetPath Path { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AspnetPersonalizationPerUsers")]
    public virtual AspnetUser User { get; set; }
}
