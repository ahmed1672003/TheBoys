using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TheBoys.Domain.Entities.AspnetApplications;
using TheBoys.Domain.Entities.AspnetUsers;

namespace TheBoys.Domain.Entities.AspnetRoles;

[Table("aspnet_Roles")]
public partial class AspnetRole
{
    public Guid ApplicationId { get; set; }

    [Key]
    public Guid RoleId { get; set; }

    [Required]
    [StringLength(256)]
    public string RoleName { get; set; }

    [Required]
    [StringLength(256)]
    public string LoweredRoleName { get; set; }

    [StringLength(256)]
    public string Description { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("AspnetRoles")]
    public virtual AspnetApplication Application { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<AspnetUser> Users { get; set; } = new List<AspnetUser>();
}
