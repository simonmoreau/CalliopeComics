using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_group_membership")]
[Index("CharacterId", Name = "idx_gcd_group_membership_gcd_group_membership_character_id_ce34b6e6_fk_gcd_character_id")]
[Index("GroupId", Name = "idx_gcd_group_membership_gcd_group_membership_group_id_ee6c8c22_fk_gcd_group_id")]
[Index("MembershipTypeId", Name = "idx_gcd_group_membership_gcd_group_membership_membership_type_id_32854b66_fk_gcd_group")]
[Index("Modified", Name = "idx_gcd_group_membership_gcd_group_membership_modified_1b391fa5")]
public partial class GcdGroupMembership
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [Column("year_joined")]
    public int? YearJoined { get; set; }

    [Column("year_joined_uncertain")]
    public int YearJoinedUncertain { get; set; }

    [Column("year_left")]
    public int? YearLeft { get; set; }

    [Column("year_left_uncertain")]
    public int YearLeftUncertain { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("character_id")]
    public int CharacterId { get; set; }

    [Column("group_id")]
    public int GroupId { get; set; }

    [Column("membership_type_id")]
    public int MembershipTypeId { get; set; }

    [ForeignKey("CharacterId")]
    [InverseProperty("GcdGroupMemberships")]
    public virtual GcdCharacter Character { get; set; } = null!;

    [ForeignKey("GroupId")]
    [InverseProperty("GcdGroupMemberships")]
    public virtual GcdGroup Group { get; set; } = null!;

    [ForeignKey("MembershipTypeId")]
    [InverseProperty("GcdGroupMemberships")]
    public virtual GcdGroupMembershipType MembershipType { get; set; } = null!;
}
