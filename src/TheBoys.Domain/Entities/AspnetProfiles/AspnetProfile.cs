using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TheBoys.Domain.Entities.AspnetUsers;

namespace TheBoys.Domain.Entities.AspnetProfiles;

[Table("aspnet_Profile")]
public partial class AspnetProfile
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    [Column(TypeName = "ntext")]
    public string PropertyNames { get; set; }

    [Required]
    [Column(TypeName = "ntext")]
    public string PropertyValuesString { get; set; }

    [Required]
    [Column(TypeName = "image")]
    public byte[] PropertyValuesBinary { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AspnetProfile")]
    public virtual AspnetUser User { get; set; }
}
