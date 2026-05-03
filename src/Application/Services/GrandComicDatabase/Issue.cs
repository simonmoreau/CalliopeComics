using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.Services.GrandComicDatabase
{
    public class Issue
    {
        [JsonPropertyName("api_url")]
        public string? ApiUrl { get; set; }

        [JsonPropertyName("series_name")]
        public string? SeriesName { get; set; }

        [JsonPropertyName("descriptor")]
        public string? Descriptor { get; set; }

        [JsonPropertyName("number")]
        public string? Number { get; set; }

        [JsonPropertyName("volume")]
        public string? Volume { get; set; }

        [JsonPropertyName("variant_name")]
        public string? VariantName { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("publication_date")]
        public string? PublicationDate { get; set; }

        [JsonPropertyName("key_date")]
        public string? KeyDate { get; set; }

        [JsonPropertyName("price")]
        public string? Price { get; set; }

        [JsonPropertyName("page_count")]
        public string? PageCount { get; set; }

        [JsonPropertyName("editing")]
        public string? Editing { get; set; }

        [JsonPropertyName("indicia_publisher")]
        public string? IndiciaPublisher { get; set; }

        [JsonPropertyName("brand_emblem")]
        public string? BrandEmblem { get; set; }

        [JsonPropertyName("isbn")]
        public string? Isbn { get; set; }

        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("rating")]
        public string? Rating { get; set; }

        [JsonPropertyName("on_sale_date")]
        public string? OnSaleDate { get; set; }

        [JsonPropertyName("indicia_frequency")]
        public string? IndiciaFrequency { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("variant_of")]
        public object? VariantOf { get; set; }

        [JsonPropertyName("series")]
        public string? Series { get; set; }

        [JsonPropertyName("indicia_printer")]
        public string? IndiciaPrinter { get; set; }

        [JsonPropertyName("keywords")]
        public string? Keywords { get; set; }

        [JsonPropertyName("story_set")]
        public List<StorySet>? StorySet { get; set; }

        [JsonPropertyName("cover")]
        public string? Cover { get; set; }
    }

    public class StorySet
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("feature")]
        public string? Feature { get; set; }

        [JsonPropertyName("sequence_number")]
        public int SequenceNumber { get; set; }

        [JsonPropertyName("page_count")]
        public string? PageCount { get; set; }

        [JsonPropertyName("script")]
        public string? Script { get; set; }

        [JsonPropertyName("pencils")]
        public string? Pencils { get; set; }

        [JsonPropertyName("inks")]
        public string? Inks { get; set; }

        [JsonPropertyName("colors")]
        public string? Colors { get; set; }

        [JsonPropertyName("letters")]
        public string? Letters { get; set; }

        [JsonPropertyName("editing")]
        public string? Editing { get; set; }

        [JsonPropertyName("job_number")]
        public string? JobNumber { get; set; }

        [JsonPropertyName("genre")]
        public string? Genre { get; set; }

        [JsonPropertyName("first_line")]
        public string? FirstLine { get; set; }

        [JsonPropertyName("characters")]
        public string? Characters { get; set; }

        [JsonPropertyName("synopsis")]
        public string? Synopsis { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("keywords")]
        public string? Keywords { get; set; }
    }


}
