using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_story_feature_object")]
[Index("StoryId", "FeatureId", IsUnique = true)]
[Index("FeatureId", Name = "idx_gcd_story_feature_object_gcd_story_feature_object_feature_id_fea4dbf9_fk_gcd_feature_id")]
public partial class GcdStoryFeatureObject
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("story_id")]
    public int StoryId { get; set; }

    [Column("feature_id")]
    public int FeatureId { get; set; }

    [ForeignKey("FeatureId")]
    [InverseProperty("GcdStoryFeatureObjects")]
    public virtual GcdFeature Feature { get; set; } = null!;

    [ForeignKey("StoryId")]
    [InverseProperty("GcdStoryFeatureObjects")]
    public virtual GcdStory Story { get; set; } = null!;
}
