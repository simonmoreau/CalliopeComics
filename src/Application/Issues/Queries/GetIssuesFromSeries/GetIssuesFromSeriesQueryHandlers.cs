using Application.Common.Interfaces;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Issues.Queries.GetIssuesFromSeries
{
    public class GetIssuesFromSeriesQueryHandlers : IAuthenticatedRequestHandler<GetIssuesFromSeriesQuery, List<IssueDto>>
    {
        private readonly IGcdDbContext _context;
        private readonly ILogger<GetIssuesFromSeriesQueryHandlers> _logger;

        public GetIssuesFromSeriesQueryHandlers(IGcdDbContext context,
            ILogger<GetIssuesFromSeriesQueryHandlers> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<IssueDto>> Handle(GetIssuesFromSeriesQuery request, CancellationToken cancellationToken)
        {
            var issues = await _context.GcdIssues
                .Where(i => i.SeriesId == request.SeriesId)
                .Include(issue => issue.Series)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            return issues.Select(i => new IssueDto(i)).ToList();
        }
    }
}
