using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_membership_type")]
public partial class GcdMembershipType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type", TypeName = "varchar(100)")]
    public string Type { get; set; } = null!;

    [InverseProperty("MembershipType")]
    public virtual ICollection<GcdCreatorMembership> GcdCreatorMemberships { get; set; } = new List<GcdCreatorMembership>();
}
