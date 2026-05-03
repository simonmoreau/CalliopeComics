using Application.Interfaces;
using Domain.DTO;
using System.Collections.Generic;

namespace Application.Issues.Queries.GetIssuesFromSeries
{
    public class GetIssuesFromSeriesQuery : AuthenticatedRequest<List<IssueDto>>
    {
        public readonly int SeriesId;
        public GetIssuesFromSeriesQuery(int seriesId)
        {
            SeriesId = seriesId;
        }
    }
}
