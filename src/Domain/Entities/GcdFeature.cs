using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_feature")]
[Index("Deleted", Name = "idx_gcd_feature_gcd_feature_deleted_d2842928")]
[Index("Disambiguation", Name = "idx_gcd_feature_gcd_feature_disambiguation_fe7ce1a9")]
[Index("FeatureTypeId", Name = "idx_gcd_feature_gcd_feature_feature_type_id_0bd38e4d_fk_gcd_feature_type_id")]
[Index("LanguageId", Name = "idx_gcd_feature_gcd_feature_language_id_f6ca9801_fk_stddata_language_id")]
[Index("Modified", Name = "idx_gcd_feature_gcd_feature_modified_dc767107")]
[Index("Name", Name = "idx_gcd_feature_gcd_feature_name_1b482a02")]
[Index("SortName", Name = "idx_gcd_feature_gcd_feature_sort_name_aa22b333")]
[Index("YearFirstPublished", Name = "idx_gcd_feature_gcd_feature_year_created_d3f06615")]
public partial class GcdFeature
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

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("sort_name", TypeName = "varchar(255)")]
    public string SortName { get; set; } = null!;

    [Column("genre", TypeName = "varchar(255)")]
    public string Genre { get; set; } = null!;

    [Column("year_first_published")]
    public int? YearFirstPublished { get; set; }

    [Column("year_first_published_uncertain")]
    public int YearFirstPublishedUncertain { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("feature_type_id")]
    public int FeatureTypeId { get; set; }

    [Column("language_id")]
    public int LanguageId { get; set; }

    [Column("disambiguation", TypeName = "varchar(255)")]
    public string Disambiguation { get; set; } = null!;

    [ForeignKey("FeatureTypeId")]
    [InverseProperty("GcdFeatures")]
    public virtual GcdFeatureType FeatureType { get; set; } = null!;

    [InverseProperty("Feature")]
    public virtual ICollection<GcdFeatureLogo2Feature> GcdFeatureLogo2Features { get; set; } = new List<GcdFeatureLogo2Feature>();

    [InverseProperty("FromFeature")]
    public virtual ICollection<GcdFeatureRelation> GcdFeatureRelationFromFeatures { get; set; } = new List<GcdFeatureRelation>();

    [InverseProperty("ToFeature")]
    public virtual ICollection<GcdFeatureRelation> GcdFeatureRelationToFeatures { get; set; } = new List<GcdFeatureRelation>();

    [InverseProperty("Feature")]
    public virtual ICollection<GcdStoryFeatureObject> GcdStoryFeatureObjects { get; set; } = new List<GcdStoryFeatureObject>();

    [ForeignKey("LanguageId")]
    [InverseProperty("GcdFeatures")]
    public virtual StddataLanguage Language { get; set; } = null!;
}
