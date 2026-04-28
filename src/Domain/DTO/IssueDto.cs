using System;
using Domain.Entities;

namespace Domain.DTO
{
    public class IssueDto
    {
        public IssueDto(GcdIssue gcdIssue)
        {
            Id = gcdIssue.Id;
            Number = gcdIssue.Number;
            Volume = gcdIssue.Volume;
            NoVolume = gcdIssue.NoVolume;
            DisplayVolumeWithNumber = gcdIssue.DisplayVolumeWithNumber;
            SeriesId = gcdIssue.SeriesId;
            IndiciaPublisherId = gcdIssue.IndiciaPublisherId;
            IndiciaPubNotPrinted = gcdIssue.IndiciaPubNotPrinted;
            BrandId = gcdIssue.BrandId;
            NoBrand = gcdIssue.NoBrand;
            PublicationDate = gcdIssue.PublicationDate;
            KeyDate = gcdIssue.KeyDate;
            SortCode = gcdIssue.SortCode;
            Price = gcdIssue.Price;
            PageCount = gcdIssue.PageCount;
            PageCountUncertain = gcdIssue.PageCountUncertain;
            IndiciaFrequency = gcdIssue.IndiciaFrequency;
            NoIndiciaFrequency = gcdIssue.NoIndiciaFrequency;
            Editing = gcdIssue.Editing;
            NoEditing = gcdIssue.NoEditing;
            Notes = gcdIssue.Notes;
            Created = gcdIssue.Created;
            Modified = gcdIssue.Modified;
            Deleted = gcdIssue.Deleted;
            IsIndexed = gcdIssue.IsIndexed;
            Isbn = gcdIssue.Isbn;
            ValidIsbn = gcdIssue.ValidIsbn;
            NoIsbn = gcdIssue.NoIsbn;
            VariantOfId = gcdIssue.VariantOfId;
            VariantName = gcdIssue.VariantName;
            Barcode = gcdIssue.Barcode;
            NoBarcode = gcdIssue.NoBarcode;
            Title = gcdIssue.Title;
            NoTitle = gcdIssue.NoTitle;
            OnSaleDate = gcdIssue.OnSaleDate;
            OnSaleDateUncertain = gcdIssue.OnSaleDateUncertain;
            Rating = gcdIssue.Rating;
            NoRating = gcdIssue.NoRating;
            VolumeNotPrinted = gcdIssue.VolumeNotPrinted;
            IndiciaPrinterNotPrinted = gcdIssue.IndiciaPrinterNotPrinted;
            VariantCoverStatus = gcdIssue.VariantCoverStatus;
            IndiciaPrinterSourcedBy = gcdIssue.IndiciaPrinterSourcedBy;
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public string Volume { get; set; }
        public int NoVolume { get; set; }
        public int DisplayVolumeWithNumber { get; set; }
        public int SeriesId { get; set; }
        public int? IndiciaPublisherId { get; set; }
        public int IndiciaPubNotPrinted { get; set; }
        public int? BrandId { get; set; }
        public int NoBrand { get; set; }
        public string PublicationDate { get; set; }
        public string? KeyDate { get; set; }
        public int SortCode { get; set; }
        public string Price { get; set; }
        public int? PageCount { get; set; }
        public int PageCountUncertain { get; set; }
        public string IndiciaFrequency { get; set; }
        public int NoIndiciaFrequency { get; set; }
        public string Editing { get; set; }
        public int NoEditing { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int Deleted { get; set; }
        public int IsIndexed { get; set; }
        public string Isbn { get; set; }
        public string ValidIsbn { get; set; }
        public int NoIsbn { get; set; }
        public int? VariantOfId { get; set; }
        public string VariantName { get; set; }
        public string Barcode { get; set; }
        public int NoBarcode { get; set; }
        public string Title { get; set; }
        public int NoTitle { get; set; }
        public string OnSaleDate { get; set; }
        public int OnSaleDateUncertain { get; set; }
        public string Rating { get; set; }
        public int NoRating { get; set; }
        public int VolumeNotPrinted { get; set; }
        public int IndiciaPrinterNotPrinted { get; set; }
        public int VariantCoverStatus { get; set; }
        public string IndiciaPrinterSourcedBy { get; set; }
    }
}
