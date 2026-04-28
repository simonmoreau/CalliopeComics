using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_group_character")]
[Index("Deleted", Name = "idx_gcd_group_character_gcd_group_character_deleted_da9fb253")]
[Index("GroupNameId", Name = "idx_gcd_group_character_gcd_group_character_group_name_id_a01b83b2_fk_gcd_group")]
[Index("Modified", Name = "idx_gcd_group_character_gcd_group_character_modified_2e1743fd")]
[Index("StoryId", Name = "idx_gcd_group_character_gcd_group_character_story_id_4aedac36_fk_gcd_story_id")]
[Index("UniverseId", Name = "idx_gcd_group_character_gcd_group_character_universe_id_34fdd9e7_fk_gcd_universe_id")]
public partial class GcdGroupCharacter
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

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("story_id")]
    public int StoryId { get; set; }

    [Column("universe_id")]
    public int? UniverseId { get; set; }

    [Column("group_name_id")]
    public int GroupNameId { get; set; }

    [ForeignKey("GroupNameId")]
    [InverseProperty("GcdGroupCharacters")]
    public virtual GcdGroupNameDetail GroupName { get; set; } = null!;

    [ForeignKey("StoryId")]
    [InverseProperty("GcdGroupCharacters")]
    public virtual GcdStory Story { get; set; } = null!;

    [ForeignKey("UniverseId")]
    [InverseProperty("GcdGroupCharacters")]
    public virtual GcdUniverse? Universe { get; set; }
}
