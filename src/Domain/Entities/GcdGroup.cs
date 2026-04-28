using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_group")]
[Index("Deleted", Name = "idx_gcd_group_gcd_group_deleted_eb8063e4")]
[Index("Disambiguation", Name = "idx_gcd_group_gcd_group_disambiguation_4e03cb92")]
[Index("LanguageId", Name = "idx_gcd_group_gcd_group_language_id_e0935a36_fk_stddata_language_id")]
[Index("Modified", Name = "idx_gcd_group_gcd_group_modified_316b6f0c")]
[Index("Name", Name = "idx_gcd_group_gcd_group_name_0358904e")]
[Index("SortName", Name = "idx_gcd_group_gcd_group_sort_name_1922a716")]
[Index("UniverseId", Name = "idx_gcd_group_gcd_group_universe_id_e32b55ff_fk_gcd_universe_id")]
[Index("YearFirstPublished", Name = "idx_gcd_group_gcd_group_year_first_published_65064aae")]
public partial class GcdGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("sort_name", TypeName = "varchar(255)")]
    public string SortName { get; set; } = null!;

    [Column("disambiguation", TypeName = "varchar(255)")]
    public string Disambiguation { get; set; } = null!;

    [Column("year_first_published")]
    public int? YearFirstPublished { get; set; }

    [Column("year_first_published_uncertain")]
    public int YearFirstPublishedUncertain { get; set; }

    [Column("description", TypeName = "longtext")]
    public string Description { get; set; } = null!;

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("language_id")]
    public int LanguageId { get; set; }

    [Column("universe_id")]
    public int? UniverseId { get; set; }

    [InverseProperty("Group")]
    public virtual ICollection<GcdGroupMembership> GcdGroupMemberships { get; set; } = new List<GcdGroupMembership>();

    [InverseProperty("Group")]
    public virtual ICollection<GcdGroupNameDetail> GcdGroupNameDetails { get; set; } = new List<GcdGroupNameDetail>();

    [InverseProperty("FromGroup")]
    public virtual ICollection<GcdGroupRelation> GcdGroupRelationFromGroups { get; set; } = new List<GcdGroupRelation>();

    [InverseProperty("ToGroup")]
    public virtual ICollection<GcdGroupRelation> GcdGroupRelationToGroups { get; set; } = new List<GcdGroupRelation>();

    [InverseProperty("Group")]
    public virtual ICollection<GcdStoryCharacterGroup> GcdStoryCharacterGroups { get; set; } = new List<GcdStoryCharacterGroup>();

    [ForeignKey("LanguageId")]
    [InverseProperty("GcdGroups")]
    public virtual StddataLanguage Language { get; set; } = null!;

    [ForeignKey("UniverseId")]
    [InverseProperty("GcdGroups")]
    public virtual GcdUniverse? Universe { get; set; }
}
