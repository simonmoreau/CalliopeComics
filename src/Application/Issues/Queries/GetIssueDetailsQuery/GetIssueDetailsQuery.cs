using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Issues.Queries.GetIssueDetailsQuery
{

    public class GetIssueDetailsQuery : AuthenticatedRequest<GcdIssue>
    {
        public readonly int Id;
        public GetIssueDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
