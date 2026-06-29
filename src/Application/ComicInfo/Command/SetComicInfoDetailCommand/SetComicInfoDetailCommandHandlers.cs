using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Services.ComicService;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ComicInfo.Command.SetComicInfoDetailCommand
{
    public class SetComicInfoDetailCommandHandlers : IAuthenticatedRequestHandler<SetComicInfoDetailCommand, bool>
    {
        private readonly IGcdDbContext _context;
        private readonly IComicService _comicService;
        private readonly ILogger<SetComicInfoDetailCommandHandlers> _logger;

        public SetComicInfoDetailCommandHandlers(IGcdDbContext context,
            IComicService comicService,
            ILogger<SetComicInfoDetailCommandHandlers> logger)
        {
            _context = context;
            _comicService = comicService;
            _logger = logger;
        }

        public async Task<bool> Handle(SetComicInfoDetailCommand request, CancellationToken cancellationToken)
        {
            GcdIssue? issue = await _context.GcdIssues.Where(i => i.Id == request.IssueId)
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
                        .FirstOrDefaultAsync(cancellationToken);

            if (issue == null)
            {
                throw new NotFoundException(nameof(GcdIssue), request.IssueId);
            }

            // Build updated ComicInfo from the issue data and persist into the archive
            Application.Services.ComicService.ComicInfo comicInfo = _comicService.CreateComicInfo(issue, request.SeriesGroup);
            await _comicService.SaveComicInfo(comicInfo, request.ComicsPath, cancellationToken);

            _logger.LogInformation("ComicInfo updated for Issue {IssueId} in {Path}", request.IssueId, request.ComicsPath);
            return true;
        }


    }
}
