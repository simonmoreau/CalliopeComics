using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_publisher")]
[Index("Name", Name = "idx_gcd_publisher_PubName")]
[Index("YearBegan", Name = "idx_gcd_publisher_YearBegan")]
[Index("BrandCount", Name = "idx_gcd_publisher_brand_count")]
[Index("CountryId", Name = "idx_gcd_publisher_country_id")]
[Index("Deleted", Name = "idx_gcd_publisher_deleted")]
[Index("Modified", Name = "idx_gcd_publisher_gcd_publisher_modified_4da24f236c8f04fe_uniq")]
[Index("YearOverallBegan", Name = "idx_gcd_publisher_gcd_publisher_year_overall_began_281cdf77")]
[Index("YearOverallBeganUncertain", Name = "idx_gcd_publisher_gcd_publisher_year_overall_began_uncertain_7213c847")]
[Index("YearOverallEndedUncertain", Name = "idx_gcd_publisher_gcd_publisher_year_overall_ended_uncertain_e155ab76")]
[Index("IndiciaPublisherCount", Name = "idx_gcd_publisher_indicia_publisher_count")]
[Index("YearBeganUncertain", Name = "idx_gcd_publisher_year_began_uncertain")]
[Index("YearEndedUncertain", Name = "idx_gcd_publisher_year_ended_uncertain")]
public partial class GcdPublisher
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("year_began")]
    public int? YearBegan { get; set; }

    [Column("year_ended")]
    public int? YearEnded { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("url", TypeName = "varchar(255)")]
    public string Url { get; set; } = null!;

    [Column("brand_count")]
    public int BrandCount { get; set; }

    [Column("indicia_publisher_count")]
    public int IndiciaPublisherCount { get; set; }

    [Column("series_count")]
    public int SeriesCount { get; set; }

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("issue_count")]
    public int IssueCount { get; set; }

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
    [InverseProperty("GcdPublishers")]
    public virtual StddataCountry Country { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<GcdBrandGroup> GcdBrandGroups { get; set; } = new List<GcdBrandGroup>();

    [InverseProperty("Publisher")]
    public virtual ICollection<GcdBrandUse> GcdBrandUses { get; set; } = new List<GcdBrandUse>();

    [InverseProperty("Parent")]
    public virtual ICollection<GcdIndiciaPublisher> GcdIndiciaPublishers { get; set; } = new List<GcdIndiciaPublisher>();

    [InverseProperty("Publisher")]
    public virtual ICollection<GcdSeries> GcdSeries { get; set; } = new List<GcdSeries>();
}
