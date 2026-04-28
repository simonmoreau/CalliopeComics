using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_feature_logo")]
[Index("Deleted", Name = "idx_gcd_feature_logo_gcd_feature_logo_deleted_7fc639e5")]
[Index("Modified", Name = "idx_gcd_feature_logo_gcd_feature_logo_modified_a01588f8")]
[Index("Name", Name = "idx_gcd_feature_logo_gcd_feature_logo_name_fca0238c")]
[Index("SortName", Name = "idx_gcd_feature_logo_gcd_feature_logo_sort_name_fc35812c")]
[Index("YearBegan", Name = "idx_gcd_feature_logo_gcd_feature_logo_year_began_e62b6676")]
public partial class GcdFeatureLogo
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

    [Column("year_began")]
    public int? YearBegan { get; set; }

    [Column("year_ended")]
    public int? YearEnded { get; set; }

    [Column("year_began_uncertain")]
    public int YearBeganUncertain { get; set; }

    [Column("year_ended_uncertain")]
    public int YearEndedUncertain { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("generic")]
    public int Generic { get; set; }

    [InverseProperty("Featurelogo")]
    public virtual ICollection<GcdFeatureLogo2Feature> GcdFeatureLogo2Features { get; set; } = new List<GcdFeatureLogo2Feature>();

    [InverseProperty("Featurelogo")]
    public virtual ICollection<GcdStoryFeatureLogo> GcdStoryFeatureLogos { get; set; } = new List<GcdStoryFeatureLogo>();
}
