using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_brand")]
[Index("Deleted", Name = "idx_gcd_brand_deleted")]
[Index("Modified", Name = "idx_gcd_brand_gcd_brand_modified_5eff80882349f141_uniq")]
[Index("YearOverallBegan", Name = "idx_gcd_brand_gcd_brand_year_overall_began_ddc19cc8")]
[Index("YearOverallBeganUncertain", Name = "idx_gcd_brand_gcd_brand_year_overall_began_uncertain_31ef576d")]
[Index("YearOverallEndedUncertain", Name = "idx_gcd_brand_gcd_brand_year_overall_ended_uncertain_e0e3902a")]
[Index("Name", Name = "idx_gcd_brand_name")]
[Index("YearBegan", Name = "idx_gcd_brand_year_began")]
[Index("YearBeganUncertain", Name = "idx_gcd_brand_year_began_uncertain")]
[Index("YearEndedUncertain", Name = "idx_gcd_brand_year_ended_uncertain")]
public partial class GcdBrand
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("year_began")]
    public int? YearBegan { get; set; }

    [Column("year_ended")]
    public int? YearEnded { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("url", TypeName = "varchar(255)")]
    public string Url { get; set; } = null!;

    [Column("issue_count")]
    public int IssueCount { get; set; }

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("year_began_uncertain")]
    public int YearBeganUncertain { get; set; }

    [Column("year_ended_uncertain")]
    public int YearEndedUncertain { get; set; }

    [Column("year_overall_began")]
    public int? YearOverallBegan { get; set; }

    [Column("year_overall_began_uncertain")]
    public int YearOverallBeganUncertain { get; set; }

    [Column("year_overall_ended")]
    public int? YearOverallEnded { get; set; }

    [Column("year_overall_ended_uncertain")]
    public int YearOverallEndedUncertain { get; set; }

    [Column("generic")]
    public int Generic { get; set; }

    [InverseProperty("Brand")]
    public virtual ICollection<GcdBrandEmblemGroup> GcdBrandEmblemGroups { get; set; } = new List<GcdBrandEmblemGroup>();

    [InverseProperty("Emblem")]
    public virtual ICollection<GcdBrandUse> GcdBrandUses { get; set; } = new List<GcdBrandUse>();

    [InverseProperty("Brand")]
    public virtual ICollection<GcdIssueBrandEmblem> GcdIssueBrandEmblems { get; set; } = new List<GcdIssueBrandEmblem>();

    [InverseProperty("Brand")]
    public virtual ICollection<GcdIssue> GcdIssues { get; set; } = new List<GcdIssue>();
}
