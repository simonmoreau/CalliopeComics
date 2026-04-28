using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_brand_group")]
[Index("Name", Name = "idx_gcd_brand_group_gcd_brand_group_52094d6e")]
[Index("ParentId", Name = "idx_gcd_brand_group_gcd_brand_group_63f17a16")]
[Index("Deleted", Name = "idx_gcd_brand_group_gcd_brand_group_6cc99b0b")]
[Index("YearEndedUncertain", Name = "idx_gcd_brand_group_gcd_brand_group_8c53af9d")]
[Index("YearBeganUncertain", Name = "idx_gcd_brand_group_gcd_brand_group_b5b058a2")]
[Index("YearBegan", Name = "idx_gcd_brand_group_gcd_brand_group_d4f3f470")]
[Index("Modified", Name = "idx_gcd_brand_group_gcd_brand_group_modified_2a3c89f27446d469_uniq")]
[Index("YearOverallBegan", Name = "idx_gcd_brand_group_gcd_brand_group_year_overall_began_146c13e6")]
[Index("YearOverallBeganUncertain", Name = "idx_gcd_brand_group_gcd_brand_group_year_overall_began_uncertain_04003dcc")]
[Index("YearOverallEndedUncertain", Name = "idx_gcd_brand_group_gcd_brand_group_year_overall_ended_uncertain_099d9624")]
public partial class GcdBrandGroup
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

    [Column("year_began_uncertain")]
    public int YearBeganUncertain { get; set; }

    [Column("year_ended_uncertain")]
    public int YearEndedUncertain { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("url", TypeName = "varchar(255)")]
    public string Url { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("parent_id")]
    public int ParentId { get; set; }

    [Column("issue_count")]
    public int IssueCount { get; set; }

    [Column("year_overall_began")]
    public int? YearOverallBegan { get; set; }

    [Column("year_overall_began_uncertain")]
    public int YearOverallBeganUncertain { get; set; }

    [Column("year_overall_ended")]
    public int? YearOverallEnded { get; set; }

    [Column("year_overall_ended_uncertain")]
    public int YearOverallEndedUncertain { get; set; }

    [InverseProperty("Brandgroup")]
    public virtual ICollection<GcdBrandEmblemGroup> GcdBrandEmblemGroups { get; set; } = new List<GcdBrandEmblemGroup>();

    [ForeignKey("ParentId")]
    [InverseProperty("GcdBrandGroups")]
    public virtual GcdPublisher Parent { get; set; } = null!;
}
