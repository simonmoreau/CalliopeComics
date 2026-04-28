using Application.Interfaces;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Issues.Queries.SearchIssuesQuery
{
    public class SearchIssuesQuery : AuthenticatedRequest<List<IssueDto>>
    {
        public readonly string SearchTerm;
        public SearchIssuesQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}
