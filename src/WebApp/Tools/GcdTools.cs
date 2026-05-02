using System.Text.Json;
using MediatR;
using ModelContextProtocol.Server;
using System.ComponentModel;
using Domain.DTO;
using Application.Issues.Queries.SearchIssuesQuery;
using Application.Series.Queries.SearchSeriesQuery;

namespace WebApp.Tools
{
    internal class GcdTools
    {
        private readonly IMediator _mediator;

        public GcdTools(IMediator mediator)
        {
            _mediator = mediator;
        }

        [McpServerTool]
        [Description("Searches for comic book issues based on a search text.")]
        public async Task<string> SearchComicIssues([Description("The search text to find comic book issues")] string searchText = "")
        {
            SearchIssuesQuery query = new SearchIssuesQuery(searchText);
            List<IssueDto> issues = await _mediator.Send(query);

            if (issues.Count == 0)
            {
                return "No issues found.";
            }

            var simplifiedIssues = issues.Select(i => new
            {
                i.Id,
                i.SeriesName,
                i.Title,
                i.Number,
                i.PublicationDate
            });

            return JsonSerializer.Serialize(simplifiedIssues);
        }

        [McpServerTool]
        [Description("Searches for comic book series based on a search text.")]
        public async Task<string> SearchSeries([Description("The search text to find comic book series")] string searchText = "")
        {
            SearchSeriesQuery query = new SearchSeriesQuery(searchText);
            List<SeriesDto> series = await _mediator.Send(query);

            if (series.Count == 0)
            {
                return "No series found.";
            }

            var simplifiedSeries = series.Select(s => new
            {
                s.Id,
                s.Name,
                s.YearBegan,
                s.IssueCount
            });

            return JsonSerializer.Serialize(simplifiedSeries);
        }
    }
}
