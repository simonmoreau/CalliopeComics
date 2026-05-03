using Application.Issues.Queries.GetIssuesFromSeries;
using Application.Issues.Queries.SearchIssuesQuery;
using Application.Series.Queries.SearchSeriesQuery;
using Application.Services.GrandComicDatabase;
using Domain.DTO;
using MediatR;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace WebApp.Tools
{
    internal class GcdTools
    {
        private readonly IMediator _mediator;
        private readonly IGrandComicDatabaseClient _grandComicDatabaseClient;

        public GcdTools(IMediator mediator, IGrandComicDatabaseClient grandComicDatabaseClient)
        {
            _mediator = mediator;
            _grandComicDatabaseClient = grandComicDatabaseClient;
        }

        [McpServerTool]
        [Description("Searches for comic book issues based on a search text. The search text can contains a issue number for a specific search.")]
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
        [Description("Searches for comic book series based on a search text (Series name, publisher and publication year).")]
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

        [McpServerTool]
        [Description("Get all the issues of a series by its ID.")]
        public async Task<string> GetIssuesFromSeries([Description("The ID of the series")] int seriesId)
        {
            GetIssuesFromSeriesQuery query = new GetIssuesFromSeriesQuery(seriesId);
            List<IssueDto> issues = await _mediator.Send(query);

            if (issues.Count == 0)
            {
                return "No issues found for this series.";
            }

            var simplifiedIssues = issues.Select(i => new
            {
                i.Id,
                i.SeriesName,
                i.Title,
                i.Number,
                i.PublicationDate,
                i.Notes
            });

            return JsonSerializer.Serialize(simplifiedIssues);
        }

        [McpServerTool]
        [Description("Get the offical cover of a comic book issue by its ID.")]
        public async Task<ImageContentBlock> GetIssueCover([Description("The ID of the comic book issue")] int issueId)
        {
            // Implementation for getting the issue cover by ID
            CancellationToken cancellationToken = new CancellationToken();
            Issue issue = await _grandComicDatabaseClient.GetIssue(issueId, cancellationToken);
            byte[] image = await _grandComicDatabaseClient.GetIssueCover(issue, cancellationToken);

            return ImageContentBlock.FromBytes(image, "image/png");
        }

    }
}
