using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_feature_relation_type")]
[Index("Name", Name = "idx_gcd_feature_relation_type_gcd_feature_relation_type_name_5eb55141")]
public partial class GcdFeatureRelationType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("description", TypeName = "varchar(255)")]
    public string Description { get; set; } = null!;

    [Column("reverse_description", TypeName = "varchar(255)")]
    public string ReverseDescription { get; set; } = null!;

    [InverseProperty("RelationType")]
    public virtual ICollection<GcdFeatureRelation> GcdFeatureRelations { get; set; } = new List<GcdFeatureRelation>();
}
