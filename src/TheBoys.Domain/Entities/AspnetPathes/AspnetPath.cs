using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheBoys.Domain.Entities.AspnetApplications;
using TheBoys.Domain.Entities.AspnetPersonalizationAllUsers;
using TheBoys.Domain.Entities.AspnetPersonalizationPerUsers;

namespace TheBoys.Domain.Entities.AspnetPathes;

[Table("aspnet_Paths")]
public partial class AspnetPath
{
    public Guid ApplicationId { get; set; }

    [Key]
    public Guid PathId { get; set; }

    [Required]
    [StringLength(256)]
    public string Path { get; set; }

    [Required]
    [StringLength(256)]
    public string LoweredPath { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("AspnetPaths")]
    public virtual AspnetApplication Application { get; set; }

    [InverseProperty("Path")]
    public virtual AspnetPersonalizationAllUser AspnetPersonalizationAllUser { get; set; }

    [InverseProperty("Path")]
    public virtual ICollection<AspnetPersonalizationPerUser> AspnetPersonalizationPerUsers { get; set; } =
        new List<AspnetPersonalizationPerUser>();
}
