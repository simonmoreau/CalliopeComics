using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("gcd_search")]
public partial class GcdSearch
{
    [Column("entity_type")]
    public byte[]? EntityType { get; set; }

    [Column("entity_id")]
    public byte[]? EntityId { get; set; }

    [Column("publisher_id")]
    public byte[]? PublisherId { get; set; }

    [Column("series_id")]
    public byte[]? SeriesId { get; set; }

    [Column("publisher_name")]
    public byte[]? PublisherName { get; set; }

    [Column("publisher_notes")]
    public byte[]? PublisherNotes { get; set; }

    [Column("publisher_url")]
    public byte[]? PublisherUrl { get; set; }

    [Column("series_name")]
    public byte[]? SeriesName { get; set; }

    [Column("series_sort_name")]
    public byte[]? SeriesSortName { get; set; }

    [Column("series_notes")]
    public byte[]? SeriesNotes { get; set; }

    [Column("series_tracking_notes")]
    public byte[]? SeriesTrackingNotes { get; set; }

    [Column("series_format")]
    public byte[]? SeriesFormat { get; set; }

    [Column("series_publication_dates")]
    public byte[]? SeriesPublicationDates { get; set; }

    [Column("series_color")]
    public byte[]? SeriesColor { get; set; }

    [Column("series_dimensions")]
    public byte[]? SeriesDimensions { get; set; }

    [Column("series_paper_stock")]
    public byte[]? SeriesPaperStock { get; set; }

    [Column("series_binding")]
    public byte[]? SeriesBinding { get; set; }

    [Column("series_publishing_format")]
    public byte[]? SeriesPublishingFormat { get; set; }

    [Column("issue_number")]
    public byte[]? IssueNumber { get; set; }

    [Column("issue_volume")]
    public byte[]? IssueVolume { get; set; }

    [Column("issue_title")]
    public byte[]? IssueTitle { get; set; }

    [Column("issue_notes")]
    public byte[]? IssueNotes { get; set; }

    [Column("issue_editing")]
    public byte[]? IssueEditing { get; set; }

    [Column("issue_publication_date")]
    public byte[]? IssuePublicationDate { get; set; }

    [Column("issue_price")]
    public byte[]? IssuePrice { get; set; }

    [Column("issue_isbn")]
    public byte[]? IssueIsbn { get; set; }

    [Column("issue_barcode")]
    public byte[]? IssueBarcode { get; set; }

    [Column("issue_rating")]
    public byte[]? IssueRating { get; set; }

    [Column("issue_variant_name")]
    public byte[]? IssueVariantName { get; set; }
}
