using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_feature_relation")]
[Index("FromFeatureId", Name = "idx_gcd_feature_relation_gcd_feature_relation_from_feature_id_25e36fdf_fk_gcd_feature_id")]
[Index("Modified", Name = "idx_gcd_feature_relation_gcd_feature_relation_modified_896fb6c6")]
[Index("RelationTypeId", Name = "idx_gcd_feature_relation_gcd_feature_relation_relation_type_id_09ba4ac7_fk_gcd_featu")]
[Index("ToFeatureId", Name = "idx_gcd_feature_relation_gcd_feature_relation_to_feature_id_e5a20bdf_fk_gcd_feature_id")]
public partial class GcdFeatureRelation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("from_feature_id")]
    public int FromFeatureId { get; set; }

    [Column("relation_type_id")]
    public int RelationTypeId { get; set; }

    [Column("to_feature_id")]
    public int ToFeatureId { get; set; }

    [ForeignKey("FromFeatureId")]
    [InverseProperty("GcdFeatureRelationFromFeatures")]
    public virtual GcdFeature FromFeature { get; set; } = null!;

    [ForeignKey("RelationTypeId")]
    [InverseProperty("GcdFeatureRelations")]
    public virtual GcdFeatureRelationType RelationType { get; set; } = null!;

    [ForeignKey("ToFeatureId")]
    [InverseProperty("GcdFeatureRelationToFeatures")]
    public virtual GcdFeature ToFeature { get; set; } = null!;
}
