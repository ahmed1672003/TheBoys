using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TheBoys.Domain.Entities.AspnetApplications;
using TheBoys.Domain.Entities.AspnetMemberships;
using TheBoys.Domain.Entities.AspnetPersonalizationPerUsers;
using TheBoys.Domain.Entities.AspnetProfiles;
using TheBoys.Domain.Entities.AspnetRoles;

namespace TheBoys.Domain.Entities.AspnetUsers;

[Table("aspnet_Users")]
public partial class AspnetUser
{
    public Guid ApplicationId { get; set; }

    [Key]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(256)]
    public string UserName { get; set; }

    [Required]
    [StringLength(256)]
    public string LoweredUserName { get; set; }

    [StringLength(16)]
    public string MobileAlias { get; set; }

    public bool IsAnonymous { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastActivityDate { get; set; }

    public bool? LoginStatus { get; set; }

    public bool IsPasswordInformed { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("AspnetUsers")]
    public virtual AspnetApplication Application { get; set; }

    [InverseProperty("User")]
    public virtual AspnetMembership AspnetMembership { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AspnetPersonalizationPerUser> AspnetPersonalizationPerUsers { get; set; } =
        new List<AspnetPersonalizationPerUser>();

    [InverseProperty("User")]
    public virtual AspnetProfile AspnetProfile { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<AspnetRole> Roles { get; set; } = new List<AspnetRole>();
}
