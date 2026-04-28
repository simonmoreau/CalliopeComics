using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_membership")]
[Index("CreatorId", Name = "idx_gcd_creator_membership_gcd_creator_membership_3700153c")]
[Index("Deleted", Name = "idx_gcd_creator_membership_gcd_creator_membership_da602f0b")]
[Index("MembershipTypeId", Name = "idx_gcd_creator_membership_gcd_creator_membership_f0ca3e55")]
[Index("Modified", Name = "idx_gcd_creator_membership_gcd_creator_membership_modified_37ffa94043c9f144_uniq")]
public partial class GcdCreatorMembership
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("organization_name", TypeName = "varchar(200)")]
    public string OrganizationName { get; set; } = null!;

    [Column("membership_year_began")]
    public int? MembershipYearBegan { get; set; }

    [Column("membership_year_began_uncertain")]
    public int MembershipYearBeganUncertain { get; set; }

    [Column("membership_year_ended")]
    public int? MembershipYearEnded { get; set; }

    [Column("membership_year_ended_uncertain")]
    public int MembershipYearEndedUncertain { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("membership_type_id")]
    public int? MembershipTypeId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdCreatorMemberships")]
    public virtual GcdCreator Creator { get; set; } = null!;

    [ForeignKey("MembershipTypeId")]
    [InverseProperty("GcdCreatorMemberships")]
    public virtual GcdMembershipType? MembershipType { get; set; }
}
