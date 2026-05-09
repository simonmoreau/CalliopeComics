using Application.Common.Interfaces;
using Application.Services.ComicService;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Application.Issues.Queries.GetIssueDetailsQuery
{

    public class GetIssueDetailsQueryHandlers : IAuthenticatedRequestHandler<GetIssueDetailsQuery, GcdIssue>
    {
        private readonly IGcdDbContext _context;
        private readonly ILogger<GetIssueDetailsQueryHandlers> _logger;
        private readonly IComicService _comicService;

        public GetIssueDetailsQueryHandlers(IGcdDbContext context, IComicService comicService,
            ILogger<GetIssueDetailsQueryHandlers> logger)
        {
            _context = context;
            _comicService = comicService;
            _logger = logger;
        }

        public async Task<GcdIssue> Handle(GetIssueDetailsQuery request, CancellationToken cancellationToken)
        {

            GcdIssue issue = await _context.GcdIssues.Where(i => i.Id == request.Id)
                    .Include(issue => issue.Series)
                            .ThenInclude(series => series.Publisher)
                        .Include(issue => issue.Series)
                            .ThenInclude(series => series.Language)
                        .Include(issue => issue.Series)
                            .ThenInclude(series => series.PublicationType)
                        .Include(issue => issue.VariantOf)
                            .ThenInclude(variantOf => variantOf.Series)
                        .Include(issue => issue.IndiciaPublisher)
                        .Include(issue => issue.GcdStories)
                            .ThenInclude(s => s.GcdStoryCharacters).ThenInclude(caracter => caracter.Character)
                         .Include(issue => issue.GcdStories)
                            .ThenInclude(s => s.GcdStoryCharacters).ThenInclude(caracter => caracter.Role)
                         .Include(issue => issue.GcdStories)
                            .ThenInclude(s => s.GcdGroupCharacters).ThenInclude(group => group.GroupName)
                        .Include(issue => issue.GcdStories)
                            .ThenInclude(s => s.GcdStoryCredits).ThenInclude(credit => credit.CreditType)
                        .Include(issue => issue.GcdStories)
                            .ThenInclude(s => s.GcdStoryCredits).ThenInclude(credit => credit.Creator).ThenInclude(c => c.Creator)
                        .Include(issue => issue.GcdIssueCredits)
                            .ThenInclude(credit => credit.CreditType)
                        .Include(issue => issue.GcdIssueCredits)
                            .ThenInclude(credit => credit.Creator).ThenInclude(c => c.Creator)
                        .AsSplitQuery()
                        .FirstAsync();

            ComicInfo comicInfo = _comicService.CreateComicInfo(issue);

            return issue;
        }
    }
}
