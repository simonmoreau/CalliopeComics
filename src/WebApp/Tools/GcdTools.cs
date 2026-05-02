using System.Text.Json;
using MediatR;
using ModelContextProtocol.Server;
using System.ComponentModel;
using Domain.DTO;
using Application.Issues.Queries.SearchIssuesQuery;

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
    }
}
