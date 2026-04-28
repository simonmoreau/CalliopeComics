using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_feature_logo_2_feature")]
[Index("FeaturelogoId", "FeatureId", IsUnique = true)]
[Index("FeatureId", Name = "idx_gcd_feature_logo_2_feature_gcd_feature_logo_2_feature_feature_id_8ec96fc1_fk_gcd_feature_id")]
public partial class GcdFeatureLogo2Feature
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("featurelogo_id")]
    public int FeaturelogoId { get; set; }

    [Column("feature_id")]
    public int FeatureId { get; set; }

    [ForeignKey("FeatureId")]
    [InverseProperty("GcdFeatureLogo2Features")]
    public virtual GcdFeature Feature { get; set; } = null!;

    [ForeignKey("FeaturelogoId")]
    [InverseProperty("GcdFeatureLogo2Features")]
    public virtual GcdFeatureLogo Featurelogo { get; set; } = null!;
}
