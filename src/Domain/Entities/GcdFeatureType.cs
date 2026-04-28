using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_feature_type")]
[Index("Name", Name = "idx_gcd_feature_type_gcd_feature_type_name_af6c2592")]
public partial class GcdFeatureType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [InverseProperty("FeatureType")]
    public virtual ICollection<GcdFeature> GcdFeatures { get; set; } = new List<GcdFeature>();
}
