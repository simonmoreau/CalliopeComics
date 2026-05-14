using System;
using Domain.Entities;

namespace Domain.DTO
{
    public class IssueSimplifiedDto
    {
        public IssueSimplifiedDto(GcdIssue gcdIssue)
        {
            Id = gcdIssue.Id;
            Number = gcdIssue.Number;
            //Volume = gcdIssue.Volume;
            //NoVolume = gcdIssue.NoVolume;
            //PublicationDate = gcdIssue.PublicationDate;
            //KeyDate = gcdIssue.KeyDate;
            //Price = gcdIssue.Price;
            //PageCount = gcdIssue.PageCount;
            //Notes = gcdIssue.Notes;
            //Title = gcdIssue.Title;
            //OnSaleDate = gcdIssue.OnSaleDate;
        }

        public int Id { get; set; }
        public string Number { get; set; }
        //public string Volume { get; set; }
        //public int NoVolume { get; set; }
        //public string PublicationDate { get; set; }
        //public string? KeyDate { get; set; }
        //public string Price { get; set; }
        //public int? PageCount { get; set; }
        //public string Notes { get; set; }
        //public string Title { get; set; }
        //public string OnSaleDate { get; set; }
    }
}
