using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_story_character_group")]
[Index("StorycharacterId", "GroupId", IsUnique = true)]
[Index("GroupId", Name = "idx_gcd_story_character_group_gcd_story_character_group_group_id_191387e6_fk_gcd_group_id")]
public partial class GcdStoryCharacterGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("storycharacter_id")]
    public int StorycharacterId { get; set; }

    [Column("group_id")]
    public int GroupId { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("GcdStoryCharacterGroups")]
    public virtual GcdGroup Group { get; set; } = null!;

    [ForeignKey("StorycharacterId")]
    [InverseProperty("GcdStoryCharacterGroups")]
    public virtual GcdStoryCharacter Storycharacter { get; set; } = null!;
}
