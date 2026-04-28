using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_story_feature_logo")]
[Index("StoryId", "FeaturelogoId", IsUnique = true)]
[Index("FeaturelogoId", Name = "idx_gcd_story_feature_logo_gcd_story_feature_lo_featurelogo_id_169a1ec3_fk_gcd_featu")]
public partial class GcdStoryFeatureLogo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("story_id")]
    public int StoryId { get; set; }

    [Column("featurelogo_id")]
    public int FeaturelogoId { get; set; }

    [ForeignKey("FeaturelogoId")]
    [InverseProperty("GcdStoryFeatureLogos")]
    public virtual GcdFeatureLogo Featurelogo { get; set; } = null!;

    [ForeignKey("StoryId")]
    [InverseProperty("GcdStoryFeatureLogos")]
    public virtual GcdStory Story { get; set; } = null!;
}
