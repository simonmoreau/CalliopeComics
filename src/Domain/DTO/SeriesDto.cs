using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.DTO
{
    public class SeriesDto
    {
        public SeriesDto(GcdSeries gcdSeries)
        {
            Id = gcdSeries.Id;
            Name = gcdSeries.Name;
            SortName = gcdSeries.SortName;
            Format = gcdSeries.Format;
            YearBegan = gcdSeries.YearBegan;
            YearBeganUncertain = gcdSeries.YearBeganUncertain;
            YearEnded = gcdSeries.YearEnded;
            YearEndedUncertain = gcdSeries.YearEndedUncertain;
            PublicationDates = gcdSeries.PublicationDates;
            FirstIssueId = gcdSeries.FirstIssueId;
            LastIssueId = gcdSeries.LastIssueId;
            IsCurrent = gcdSeries.IsCurrent;
            PublisherId = gcdSeries.PublisherId;
            CountryId = gcdSeries.CountryId;
            LanguageId = gcdSeries.LanguageId;
            TrackingNotes = gcdSeries.TrackingNotes;
            Notes = gcdSeries.Notes;
            HasGallery = gcdSeries.HasGallery;
            IssueCount = gcdSeries.IssueCount;
            Created = gcdSeries.Created;
            Modified = gcdSeries.Modified;
            Deleted = gcdSeries.Deleted;
            HasIndiciaFrequency = gcdSeries.HasIndiciaFrequency;
            HasIsbn = gcdSeries.HasIsbn;
            HasBarcode = gcdSeries.HasBarcode;
            HasIssueTitle = gcdSeries.HasIssueTitle;
            HasVolume = gcdSeries.HasVolume;
            IsComicsPublication = gcdSeries.IsComicsPublication;
            Color = gcdSeries.Color;
            Dimensions = gcdSeries.Dimensions;
            PaperStock = gcdSeries.PaperStock;
            Binding = gcdSeries.Binding;
            PublishingFormat = gcdSeries.PublishingFormat;
            HasRating = gcdSeries.HasRating;
            PublicationTypeId = gcdSeries.PublicationTypeId;
            IsSingleton = gcdSeries.IsSingleton;
            HasAboutComics = gcdSeries.HasAboutComics;
            HasIndiciaPrinter = gcdSeries.HasIndiciaPrinter;
            HasPublisherCodeNumber = gcdSeries.HasPublisherCodeNumber;
            Issues = gcdSeries.GcdIssues?.Select(i => new IssueSimplifiedDto(i)).ToList() ?? new List<IssueSimplifiedDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SortName { get; set; }
        public string Format { get; set; }
        public int YearBegan { get; set; }
        public int YearBeganUncertain { get; set; }
        public int? YearEnded { get; set; }
        public int YearEndedUncertain { get; set; }
        public string PublicationDates { get; set; }
        public int? FirstIssueId { get; set; }
        public int? LastIssueId { get; set; }
        public int IsCurrent { get; set; }
        public int PublisherId { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }
        public string TrackingNotes { get; set; }
        public string Notes { get; set; }
        public int HasGallery { get; set; }
        public int IssueCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int Deleted { get; set; }
        public int HasIndiciaFrequency { get; set; }
        public int HasIsbn { get; set; }
        public int HasBarcode { get; set; }
        public int HasIssueTitle { get; set; }
        public int HasVolume { get; set; }
        public int IsComicsPublication { get; set; }
        public string Color { get; set; }
        public string Dimensions { get; set; }
        public string PaperStock { get; set; }
        public string Binding { get; set; }
        public string PublishingFormat { get; set; }
        public int HasRating { get; set; }
        public int? PublicationTypeId { get; set; }
        public int IsSingleton { get; set; }
        public int HasAboutComics { get; set; }
        public int HasIndiciaPrinter { get; set; }
        public int HasPublisherCodeNumber { get; set; }
        public List<IssueSimplifiedDto> Issues { get; set; }
    }
}
