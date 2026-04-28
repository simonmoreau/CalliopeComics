using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_story_universe")]
[Index("StoryId", "UniverseId", IsUnique = true)]
[Index("UniverseId", Name = "idx_gcd_story_universe_gcd_story_universe_universe_id_d3185012_fk_gcd_universe_id")]
public partial class GcdStoryUniverse
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("story_id")]
    public int StoryId { get; set; }

    [Column("universe_id")]
    public int UniverseId { get; set; }

    [ForeignKey("StoryId")]
    [InverseProperty("GcdStoryUniverses")]
    public virtual GcdStory Story { get; set; } = null!;

    [ForeignKey("UniverseId")]
    [InverseProperty("GcdStoryUniverses")]
    public virtual GcdUniverse Universe { get; set; } = null!;
}
