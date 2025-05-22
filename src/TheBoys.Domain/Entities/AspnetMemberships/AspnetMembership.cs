using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TheBoys.Domain.Entities.AspnetApplications;
using TheBoys.Domain.Entities.AspnetUsers;

namespace TheBoys.Domain.Entities.AspnetMemberships;

[Table("aspnet_Membership")]
public partial class AspnetMembership
{
    public Guid ApplicationId { get; set; }

    [Key]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(128)]
    public string Password { get; set; }

    public int PasswordFormat { get; set; }

    [Required]
    [StringLength(128)]
    public string PasswordSalt { get; set; }

    [Column("MobilePIN")]
    [StringLength(16)]
    public string MobilePin { get; set; }

    [StringLength(256)]
    public string Email { get; set; }

    [StringLength(256)]
    public string LoweredEmail { get; set; }

    [StringLength(256)]
    public string PasswordQuestion { get; set; }

    [StringLength(128)]
    public string PasswordAnswer { get; set; }

    public bool IsApproved { get; set; }

    public bool IsLockedOut { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastLoginDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastPasswordChangedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastLockoutDate { get; set; }

    public int FailedPasswordAttemptCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FailedPasswordAttemptWindowStart { get; set; }

    public int FailedPasswordAnswerAttemptCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

    [Column(TypeName = "ntext")]
    public string Comment { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("AspnetMemberships")]
    public virtual AspnetApplication Application { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AspnetMembership")]
    public virtual AspnetUser User { get; set; }
}
