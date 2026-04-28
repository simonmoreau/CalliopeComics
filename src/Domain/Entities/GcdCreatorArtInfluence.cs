using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_art_influence")]
[Index("CreatorId", Name = "idx_gcd_creator_art_influence_gcd_creator_art_influence_3700153c")]
[Index("Deleted", Name = "idx_gcd_creator_art_influence_gcd_creator_art_influence_da602f0b")]
[Index("InfluenceLinkId", Name = "idx_gcd_creator_art_influence_gcd_creator_art_influence_ea572c03")]
[Index("Modified", Name = "idx_gcd_creator_art_influence_gcd_creator_art_influence_modified_1c0f19408432804c_uniq")]
public partial class GcdCreatorArtInfluence
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("influence_name", TypeName = "varchar(200)")]
    public string InfluenceName { get; set; } = null!;

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

    [Column("influence_link_id")]
    public int? InfluenceLinkId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdCreatorArtInfluenceCreators")]
    public virtual GcdCreator Creator { get; set; } = null!;

    [ForeignKey("InfluenceLinkId")]
    [InverseProperty("GcdCreatorArtInfluenceInfluenceLinks")]
    public virtual GcdCreator? InfluenceLink { get; set; }
}
