using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_story_character")]
[Index("CharacterId", Name = "idx_gcd_story_character_gcd_story_character_character_id_eac50f28_fk_gcd_chara")]
[Index("Deleted", Name = "idx_gcd_story_character_gcd_story_character_deleted_5b1dd00b")]
[Index("GroupUniverseId", Name = "idx_gcd_story_character_gcd_story_character_group_universe_id_1660ef69_fk_gcd_unive")]
[Index("IsDeath", Name = "idx_gcd_story_character_gcd_story_character_is_death_a3861547")]
[Index("IsFlashback", Name = "idx_gcd_story_character_gcd_story_character_is_flashback_3898577a")]
[Index("IsOrigin", Name = "idx_gcd_story_character_gcd_story_character_is_origin_17b5e411")]
[Index("Modified", Name = "idx_gcd_story_character_gcd_story_character_modified_6f623117")]
[Index("RoleId", Name = "idx_gcd_story_character_gcd_story_character_role_id_f7883e66_fk_gcd_character_role_id")]
[Index("StoryId", Name = "idx_gcd_story_character_gcd_story_character_story_id_13ce6388_fk_gcd_story_id")]
[Index("UniverseId", Name = "idx_gcd_story_character_gcd_story_character_universe_id_a98c97a2_fk_gcd_universe_id")]
public partial class GcdStoryCharacter
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

    [Column("is_flashback")]
    public int IsFlashback { get; set; }

    [Column("is_origin")]
    public int IsOrigin { get; set; }

    [Column("is_death")]
    public int IsDeath { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("character_id")]
    public int CharacterId { get; set; }

    [Column("role_id")]
    public int? RoleId { get; set; }

    [Column("story_id")]
    public int StoryId { get; set; }

    [Column("universe_id")]
    public int? UniverseId { get; set; }

    [Column("group_universe_id")]
    public int? GroupUniverseId { get; set; }

    [ForeignKey("CharacterId")]
    [InverseProperty("GcdStoryCharacters")]
    public virtual GcdCharacterNameDetail Character { get; set; } = null!;

    [InverseProperty("Storycharacter")]
    public virtual ICollection<GcdStoryCharacterGroup> GcdStoryCharacterGroups { get; set; } = new List<GcdStoryCharacterGroup>();

    [ForeignKey("GroupUniverseId")]
    [InverseProperty("GcdStoryCharacterGroupUniverses")]
    public virtual GcdUniverse? GroupUniverse { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("GcdStoryCharacters")]
    public virtual GcdCharacterRole? Role { get; set; }

    [ForeignKey("StoryId")]
    [InverseProperty("GcdStoryCharacters")]
    public virtual GcdStory Story { get; set; } = null!;

    [ForeignKey("UniverseId")]
    [InverseProperty("GcdStoryCharacterUniverses")]
    public virtual GcdUniverse? Universe { get; set; }
}
