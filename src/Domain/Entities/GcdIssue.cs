using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_issue")]
[Index("SeriesId", "SortCode", IsUnique = true)]
[Index("Number", Name = "idx_gcd_issue_Issue")]
[Index("KeyDate", Name = "idx_gcd_issue_Key_Date")]
[Index("Modified", Name = "idx_gcd_issue_Modified")]
[Index("SeriesId", Name = "idx_gcd_issue_SeriesID")]
[Index("Volume", Name = "idx_gcd_issue_VolumeNum")]
[Index("Barcode", Name = "idx_gcd_issue_barcode")]
[Index("BrandId", Name = "idx_gcd_issue_brand_id")]
[Index("Deleted", Name = "idx_gcd_issue_deleted")]
[Index("DisplayVolumeWithNumber", Name = "idx_gcd_issue_display_volume_with_number")]
[Index("Rating", Name = "idx_gcd_issue_gcd_issue_1a619ca6")]
[Index("NoRating", Name = "idx_gcd_issue_gcd_issue_ed4c6b73")]
[Index("VariantCoverStatus", Name = "idx_gcd_issue_gcd_issue_variant_cover_status_52969645")]
[Index("IndiciaPublisherId", Name = "idx_gcd_issue_indicia_publisher_id")]
[Index("IsIndexed", Name = "idx_gcd_issue_is_indexed")]
[Index("Isbn", Name = "idx_gcd_issue_isbn")]
[Index("NoBrand", Name = "idx_gcd_issue_no_brand")]
[Index("NoEditing", Name = "idx_gcd_issue_no_editing")]
[Index("NoIndiciaFrequency", Name = "idx_gcd_issue_no_indicia_frequency")]
[Index("NoIsbn", Name = "idx_gcd_issue_no_isbn")]
[Index("NoTitle", Name = "idx_gcd_issue_no_title")]
[Index("NoVolume", Name = "idx_gcd_issue_no_volume")]
[Index("OnSaleDate", Name = "idx_gcd_issue_on_sale_date")]
[Index("SortCode", Name = "idx_gcd_issue_sort_code")]
[Index("Title", Name = "idx_gcd_issue_title")]
[Index("ValidIsbn", Name = "idx_gcd_issue_valid_isbn")]
[Index("VariantOfId", Name = "idx_gcd_issue_variant_of_id")]
public partial class GcdIssue
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("number", TypeName = "varchar(50)")]
    public string Number { get; set; } = null!;

    [Column("volume", TypeName = "varchar(50)")]
    public string Volume { get; set; } = null!;

    [Column("no_volume")]
    public int NoVolume { get; set; }

    [Column("display_volume_with_number")]
    public int DisplayVolumeWithNumber { get; set; }

    [Column("series_id")]
    public int SeriesId { get; set; }

    [Column("indicia_publisher_id")]
    public int? IndiciaPublisherId { get; set; }

    [Column("indicia_pub_not_printed")]
    public int IndiciaPubNotPrinted { get; set; }

    [Column("brand_id")]
    public int? BrandId { get; set; }

    [Column("no_brand")]
    public int NoBrand { get; set; }

    [Column("publication_date", TypeName = "varchar(255)")]
    public string PublicationDate { get; set; } = null!;

    [Column("key_date", TypeName = "varchar(10)")]
    public DateOnly KeyDate { get; set; }

    [Column("sort_code")]
    public int SortCode { get; set; }

    [Column("price", TypeName = "varchar(255)")]
    public string Price { get; set; } = null!;

    [Column("page_count", TypeName = "decimal(10,3)")]
    public int? PageCount { get; set; }

    [Column("page_count_uncertain")]
    public int PageCountUncertain { get; set; }

    [Column("indicia_frequency", TypeName = "varchar(255)")]
    public string IndiciaFrequency { get; set; } = null!;

    [Column("no_indicia_frequency")]
    public int NoIndiciaFrequency { get; set; }

    [Column("editing", TypeName = "longtext")]
    public string Editing { get; set; } = null!;

    [Column("no_editing")]
    public int NoEditing { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("is_indexed")]
    public int IsIndexed { get; set; }

    [Column("isbn", TypeName = "varchar(32)")]
    public string Isbn { get; set; } = null!;

    [Column("valid_isbn", TypeName = "varchar(13)")]
    public string ValidIsbn { get; set; } = null!;

    [Column("no_isbn")]
    public int NoIsbn { get; set; }

    [Column("variant_of_id")]
    public int? VariantOfId { get; set; }

    [Column("variant_name", TypeName = "varchar(255)")]
    public string VariantName { get; set; } = null!;

    [Column("barcode", TypeName = "varchar(38)")]
    public string Barcode { get; set; } = null!;

    [Column("no_barcode")]
    public int NoBarcode { get; set; }

    [Column("title", TypeName = "varchar(255)")]
    public string Title { get; set; } = null!;

    [Column("no_title")]
    public int NoTitle { get; set; }

    [Column("on_sale_date", TypeName = "varchar(10)")]
    public string OnSaleDate { get; set; } = null!;

    [Column("on_sale_date_uncertain")]
    public int OnSaleDateUncertain { get; set; }

    [Column("rating", TypeName = "varchar(255)")]
    public string Rating { get; set; } = null!;

    [Column("no_rating")]
    public int NoRating { get; set; }

    [Column("volume_not_printed")]
    public int VolumeNotPrinted { get; set; }

    [Column("indicia_printer_not_printed")]
    public int IndiciaPrinterNotPrinted { get; set; }

    [Column("variant_cover_status")]
    public int VariantCoverStatus { get; set; }

    [Column("indicia_printer_sourced_by", TypeName = "varchar(255)")]
    public string IndiciaPrinterSourcedBy { get; set; } = null!;

    [ForeignKey("BrandId")]
    [InverseProperty("GcdIssues")]
    public virtual GcdBrand? Brand { get; set; }

    [InverseProperty("Issue")]
    public virtual ICollection<GcdIssueBrandEmblem> GcdIssueBrandEmblems { get; set; } = new List<GcdIssueBrandEmblem>();

    [InverseProperty("Issue")]
    public virtual ICollection<GcdIssueCredit> GcdIssueCredits { get; set; } = new List<GcdIssueCredit>();

    [InverseProperty("Issue")]
    public virtual ICollection<GcdIssueIndiciaPrinter> GcdIssueIndiciaPrinters { get; set; } = new List<GcdIssueIndiciaPrinter>();

    [InverseProperty("OriginIssue")]
    public virtual ICollection<GcdReprint> GcdReprintOriginIssues { get; set; } = new List<GcdReprint>();

    [InverseProperty("TargetIssue")]
    public virtual ICollection<GcdReprint> GcdReprintTargetIssues { get; set; } = new List<GcdReprint>();

    [InverseProperty("OriginIssue")]
    public virtual ICollection<GcdSeriesBond> GcdSeriesBondOriginIssues { get; set; } = new List<GcdSeriesBond>();

    [InverseProperty("TargetIssue")]
    public virtual ICollection<GcdSeriesBond> GcdSeriesBondTargetIssues { get; set; } = new List<GcdSeriesBond>();

    [InverseProperty("FirstIssue")]
    public virtual ICollection<GcdSeries> GcdSeriesFirstIssues { get; set; } = new List<GcdSeries>();

    [InverseProperty("LastIssue")]
    public virtual ICollection<GcdSeries> GcdSeriesLastIssues { get; set; } = new List<GcdSeries>();

    [InverseProperty("Issue")]
    public virtual ICollection<GcdStory> GcdStories { get; set; } = new List<GcdStory>();

    [ForeignKey("IndiciaPublisherId")]
    [InverseProperty("GcdIssues")]
    public virtual GcdIndiciaPublisher? IndiciaPublisher { get; set; }

    [InverseProperty("VariantOf")]
    public virtual ICollection<GcdIssue> InverseVariantOf { get; set; } = new List<GcdIssue>();

    [ForeignKey("SeriesId")]
    [InverseProperty("GcdIssues")]
    public virtual GcdSeries Series { get; set; } = null!;

    [ForeignKey("VariantOfId")]
    [InverseProperty("InverseVariantOf")]
    public virtual GcdIssue? VariantOf { get; set; }
}
