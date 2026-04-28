using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_indicia_publisher")]
[Index("CountryId", Name = "idx_gcd_indicia_publisher_country_id")]
[Index("Deleted", Name = "idx_gcd_indicia_publisher_deleted")]
[Index("Modified", Name = "idx_gcd_indicia_publisher_gcd_indicia_publisher_modified_43690202d96566e2_uniq")]
[Index("YearOverallBegan", Name = "idx_gcd_indicia_publisher_gcd_indicia_publisher_year_overall_began_854f12bb")]
[Index("YearOverallBeganUncertain", Name = "idx_gcd_indicia_publisher_gcd_indicia_publisher_year_overall_began_uncertain_d51c93af")]
[Index("YearOverallEndedUncertain", Name = "idx_gcd_indicia_publisher_gcd_indicia_publisher_year_overall_ended_uncertain_784b92eb")]
[Index("IsSurrogate", Name = "idx_gcd_indicia_publisher_is_surrogate")]
[Index("Name", Name = "idx_gcd_indicia_publisher_name")]
[Index("ParentId", Name = "idx_gcd_indicia_publisher_parent_id")]
[Index("YearBegan", Name = "idx_gcd_indicia_publisher_year_began")]
[Index("YearBeganUncertain", Name = "idx_gcd_indicia_publisher_year_began_uncertain")]
[Index("YearEndedUncertain", Name = "idx_gcd_indicia_publisher_year_ended_uncertain")]
public partial class GcdIndiciaPublisher
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("parent_id")]
    public int ParentId { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("year_began")]
    public int? YearBegan { get; set; }

    [Column("year_ended")]
    public int? YearEnded { get; set; }

    [Column("is_surrogate")]
    public int IsSurrogate { get; set; }

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

    [ForeignKey("CountryId")]
    [InverseProperty("GcdIndiciaPublishers")]
    public virtual StddataCountry Country { get; set; } = null!;

    [InverseProperty("IndiciaPublisher")]
    public virtual ICollection<GcdIssue> GcdIssues { get; set; } = new List<GcdIssue>();

    [ForeignKey("ParentId")]
    [InverseProperty("GcdIndiciaPublishers")]
    public virtual GcdPublisher Parent { get; set; } = null!;
}
