using Application.Clients.Queries.GetClientsQuery;
using Application.Common.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Issues.Queries.SearchIssuesQuery
{

    public class SearchIssuesQueryHandler : IAuthenticatedRequestHandler<SearchIssuesQuery, List<IssueDto>>
    {
        private readonly IGcdDbContext _context;

        public SearchIssuesQueryHandler(IGcdDbContext context)
        {
            _context = context;
        }

        public async Task<List<IssueDto>> Handle(SearchIssuesQuery request, CancellationToken cancellationToken)
        {
            List<GcdIssue> issues = await _context.GcdIssues
                .Take(100)
                .ToListAsync(cancellationToken);

            return issues.Select(issue => new IssueDto(issue)).ToList();
        }
    }
}
