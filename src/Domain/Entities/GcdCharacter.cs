using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_character")]
[Index("Deleted", Name = "idx_gcd_character_gcd_character_deleted_cf8504b7")]
[Index("Disambiguation", Name = "idx_gcd_character_gcd_character_disambiguation_6a99e15f")]
[Index("LanguageId", Name = "idx_gcd_character_gcd_character_language_id_a766f96d_fk_stddata_language_id")]
[Index("Modified", Name = "idx_gcd_character_gcd_character_modified_0364bb9a")]
[Index("Name", Name = "idx_gcd_character_gcd_character_name_018c558c")]
[Index("SortName", Name = "idx_gcd_character_gcd_character_sort_name_b009e2bf")]
[Index("UniverseId", Name = "idx_gcd_character_gcd_character_universe_id_1058453c_fk_gcd_universe_id")]
[Index("YearFirstPublished", Name = "idx_gcd_character_gcd_character_year_first_published_381260d2")]
public partial class GcdCharacter
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

    [InverseProperty("Character")]
    public virtual ICollection<GcdCharacterNameDetail> GcdCharacterNameDetails { get; set; } = new List<GcdCharacterNameDetail>();

    [InverseProperty("FromCharacter")]
    public virtual ICollection<GcdCharacterRelation> GcdCharacterRelationFromCharacters { get; set; } = new List<GcdCharacterRelation>();

    [InverseProperty("ToCharacter")]
    public virtual ICollection<GcdCharacterRelation> GcdCharacterRelationToCharacters { get; set; } = new List<GcdCharacterRelation>();

    [InverseProperty("Character")]
    public virtual ICollection<GcdGroupMembership> GcdGroupMemberships { get; set; } = new List<GcdGroupMembership>();

    [ForeignKey("LanguageId")]
    [InverseProperty("GcdCharacters")]
    public virtual StddataLanguage Language { get; set; } = null!;

    [ForeignKey("UniverseId")]
    [InverseProperty("GcdCharacters")]
    public virtual GcdUniverse? Universe { get; set; }
}
