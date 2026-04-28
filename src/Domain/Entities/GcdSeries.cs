using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_series")]
[Index("Name", Name = "idx_gcd_series_Bk_Name")]
[Index("HasGallery", Name = "idx_gcd_series_HasGallery")]
[Index("PublisherId", Name = "idx_gcd_series_PubID")]
[Index("YearBegan", Name = "idx_gcd_series_Yr_Began")]
[Index("CountryId", Name = "idx_gcd_series_country_id")]
[Index("Deleted", Name = "idx_gcd_series_deleted")]
[Index("FirstIssueId", Name = "idx_gcd_series_first_issue_id")]
[Index("PublicationTypeId", Name = "idx_gcd_series_gcd_series_49a7a4e1")]
[Index("Modified", Name = "idx_gcd_series_gcd_series_modified_6085f750f3ffa284_uniq")]
[Index("IsCurrent", Name = "idx_gcd_series_is_current")]
[Index("LanguageId", Name = "idx_gcd_series_language_id")]
[Index("LastIssueId", Name = "idx_gcd_series_last_issue_id")]
[Index("SortName", Name = "idx_gcd_series_sort_name")]
public partial class GcdSeries
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("sort_name", TypeName = "varchar(255)")]
    public string SortName { get; set; } = null!;

    [Column("format", TypeName = "varchar(255)")]
    public string Format { get; set; } = null!;

    [Column("year_began")]
    public int YearBegan { get; set; }

    [Column("year_began_uncertain")]
    public int YearBeganUncertain { get; set; }

    [Column("year_ended")]
    public int? YearEnded { get; set; }

    [Column("year_ended_uncertain")]
    public int YearEndedUncertain { get; set; }

    [Column("publication_dates", TypeName = "varchar(255)")]
    public string PublicationDates { get; set; } = null!;

    [Column("first_issue_id")]
    public int? FirstIssueId { get; set; }

    [Column("last_issue_id")]
    public int? LastIssueId { get; set; }

    [Column("is_current")]
    public int IsCurrent { get; set; }

    [Column("publisher_id")]
    public int PublisherId { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("language_id")]
    public int LanguageId { get; set; }

    [Column("tracking_notes", TypeName = "longtext")]
    public string TrackingNotes { get; set; } = null!;

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("has_gallery")]
    public int HasGallery { get; set; }

    [Column("issue_count")]
    public int IssueCount { get; set; }

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("has_indicia_frequency")]
    public int HasIndiciaFrequency { get; set; }

    [Column("has_isbn")]
    public int HasIsbn { get; set; }

    [Column("has_barcode")]
    public int HasBarcode { get; set; }

    [Column("has_issue_title")]
    public int HasIssueTitle { get; set; }

    [Column("has_volume")]
    public int HasVolume { get; set; }

    [Column("is_comics_publication")]
    public int IsComicsPublication { get; set; }

    [Column("color", TypeName = "varchar(255)")]
    public string Color { get; set; } = null!;

    [Column("dimensions", TypeName = "varchar(255)")]
    public string Dimensions { get; set; } = null!;

    [Column("paper_stock", TypeName = "varchar(255)")]
    public string PaperStock { get; set; } = null!;

    [Column("binding", TypeName = "varchar(255)")]
    public string Binding { get; set; } = null!;

    [Column("publishing_format", TypeName = "varchar(255)")]
    public string PublishingFormat { get; set; } = null!;

    [Column("has_rating")]
    public int HasRating { get; set; }

    [Column("publication_type_id")]
    public int? PublicationTypeId { get; set; }

    [Column("is_singleton")]
    public int IsSingleton { get; set; }

    [Column("has_about_comics")]
    public int HasAboutComics { get; set; }

    [Column("has_indicia_printer")]
    public int HasIndiciaPrinter { get; set; }

    [Column("has_publisher_code_number")]
    public int HasPublisherCodeNumber { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("GcdSeries")]
    public virtual StddataCountry Country { get; set; } = null!;

    [ForeignKey("FirstIssueId")]
    [InverseProperty("GcdSeriesFirstIssues")]
    public virtual GcdIssue? FirstIssue { get; set; }

    [InverseProperty("Series")]
    public virtual ICollection<GcdIssue> GcdIssues { get; set; } = new List<GcdIssue>();

    [InverseProperty("Origin")]
    public virtual ICollection<GcdSeriesBond> GcdSeriesBondOrigins { get; set; } = new List<GcdSeriesBond>();

    [InverseProperty("Target")]
    public virtual ICollection<GcdSeriesBond> GcdSeriesBondTargets { get; set; } = new List<GcdSeriesBond>();

    [ForeignKey("LanguageId")]
    [InverseProperty("GcdSeries")]
    public virtual StddataLanguage Language { get; set; } = null!;

    [ForeignKey("LastIssueId")]
    [InverseProperty("GcdSeriesLastIssues")]
    public virtual GcdIssue? LastIssue { get; set; }

    [ForeignKey("PublicationTypeId")]
    [InverseProperty("GcdSeries")]
    public virtual GcdSeriesPublicationType? PublicationType { get; set; }

    [ForeignKey("PublisherId")]
    [InverseProperty("GcdSeries")]
    public virtual GcdPublisher Publisher { get; set; } = null!;
}
