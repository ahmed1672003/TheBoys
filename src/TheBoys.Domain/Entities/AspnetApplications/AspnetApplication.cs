using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TheBoys.Domain.Entities.AspnetMemberships;
using TheBoys.Domain.Entities.AspnetPathes;
using TheBoys.Domain.Entities.AspnetRoles;
using TheBoys.Domain.Entities.AspnetUsers;

namespace TheBoys.Domain.Entities.AspnetApplications;

[Table("aspnet_Applications")]
[Index("LoweredApplicationName", Name = "UQ__aspnet_A__17477DE41ED998B2", IsUnique = true)]
[Index("ApplicationName", Name = "UQ__aspnet_A__309103311BFD2C07", IsUnique = true)]
public partial class AspnetApplication
{
    [Required]
    [StringLength(256)]
    public string ApplicationName { get; set; }

    [Required]
    [StringLength(256)]
    public string LoweredApplicationName { get; set; }

    [Key]
    public Guid ApplicationId { get; set; }

    [StringLength(256)]
    public string Description { get; set; }

    [InverseProperty("Application")]
    public virtual ICollection<AspnetMembership> AspnetMemberships { get; set; } =
        new List<AspnetMembership>();

    [InverseProperty("Application")]
    public virtual ICollection<AspnetPath> AspnetPaths { get; set; } = new List<AspnetPath>();

    [InverseProperty("Application")]
    public virtual ICollection<AspnetRole> AspnetRoles { get; set; } = new List<AspnetRole>();

    [InverseProperty("Application")]
    public virtual ICollection<AspnetUser> AspnetUsers { get; set; } = new List<AspnetUser>();
}
