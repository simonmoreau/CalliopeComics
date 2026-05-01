using Application.Common.Interfaces;
using Application.Services.ComicService;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Application.Issues.Queries.SearchIssuesQuery
{

    public class SearchIssuesQueryHandler : IAuthenticatedRequestHandler<SearchIssuesQuery, List<IssueDto>>
    {
        private readonly IGcdDbContext _context;
        private readonly ILogger<SearchIssuesQueryHandler> _logger;
        private readonly IComicService _comicService;

        public SearchIssuesQueryHandler(IGcdDbContext context, IComicService comicService,
            ILogger<SearchIssuesQueryHandler> logger)
        {
            _context = context;
            _comicService = comicService;   
            _logger = logger;
        }

        public async Task<List<IssueDto>> Handle(SearchIssuesQuery request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                return new List<IssueDto>();
            }
            
            string[] caractersToRemoves = ["<", ">", "-", ",","\"", "'", ";", ":", "&", "#", "(", ")","[","]","{","}"];
            string sanitizedSearchTerm = caractersToRemoves.Aggregate(request.SearchTerm, (current, c) => current.Replace(c, " "));
            string[] terms = sanitizedSearchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string[] normalizedTerms = terms
                .Select(NormalizeSearchTerm)
                .ToArray();


            IQueryable<GcdIssue> issuesQuery = _context.GcdIssues
                        .Include(issue => issue.Series)
                        .ThenInclude(series => series.Publisher);


            foreach (string term in normalizedTerms)
            {
                if (string.IsNullOrWhiteSpace(term)) continue;

                
                issuesQuery = issuesQuery.Where(issue =>
                        EF.Functions.Like(issue.Number, $"%{term}%") ||
                        EF.Functions.Like(issue.Volume, $"%{term}%") ||
                        EF.Functions.Like(issue.PublicationDate, $"%{term}%") ||
                        (issue.KeyDate != null && EF.Functions.Like(issue.KeyDate, $"%{term}%")) ||
                        EF.Functions.Like(issue.Editing, $"%{term}%") ||
                        EF.Functions.Like(issue.Notes, $"%{term}%") ||
                        EF.Functions.Like(issue.VariantName, $"%{term}%") ||
                        EF.Functions.Like(issue.Title, $"%{term}%") ||
                        EF.Functions.Like(issue.OnSaleDate, $"%{term}%") ||
                        EF.Functions.Like(issue.Series.Name, $"%{term}%") ||
                        EF.Functions.Like(issue.Series.PublicationDates, $"%{term}%") ||
                        EF.Functions.Like(issue.Series.Notes, $"%{term}%") ||
                        EF.Functions.Like(issue.Series.Publisher.Name, $"%{term}%")
                     );
            }


            Stopwatch stopwatch = Stopwatch.StartNew();

            List<GcdIssue> issues = await issuesQuery
                .OrderBy(i => i.Created)
                .Take(100).ToListAsync(cancellationToken);

            stopwatch.Stop();
            _logger.LogInformation("SearchIssues query executed in {ElapsedMilliseconds} ms and returned {IssueCount} issues.", stopwatch.ElapsedMilliseconds, issues.Count);

            if (issues.Count == 100)
            {
                IQueryable<GcdIssue> localizedQuery = issuesQuery
                        .Include(issue => issue.Series)
                        .ThenInclude(series => series.Language)
                        .Where(issue => issue.Series.Language.Code == "en" || issue.Series.Language.Code == "fr");

                issues = await localizedQuery
                .OrderBy(i => i.Created)
                .Take(100).ToListAsync(cancellationToken);

                _logger.LogInformation("localized SearchIssues query executed and returned {IssueCount} issues.", issues.Count);

            }

            List<IssueDto> results = issues
                .Select(issue => new
                {
                    Issue = issue,
                    Score = CalculateWeightedScore(issue, normalizedTerms)
                })
                .Where(result => result.Score > 0)
                .OrderByDescending(result => result.Score)
                .ThenByDescending(result => result.Issue.Modified)
                .Take(10)
                .Select(result => new IssueDto(result.Issue))
                .ToList();

            return results;
        }

        private static string NormalizeSearchTerm(string term)
        {
            if (int.TryParse(term, out int number))
            {
                return number.ToString();
            }

            return term;
        }

        private int CalculateWeightedScore(GcdIssue issue, string[] terms)
        {
            int score = 0;
            string issueText = BuildIssueSearchText(issue);
            string seriesText = BuildSeriesSearchText(issue.Series);
            string publisherText = BuildPublisherSearchText(issue.Series.Publisher);

            foreach (string term in terms)
            {
                if (ContainsIgnoreCase(issue.Title, term))
                {
                    score += 20;
                }

                if (ContainsIgnoreCase(issue.Number, term) || ContainsIgnoreCase(issue.Volume, term))
                {
                    score += 15;
                }

                if (ContainsIgnoreCase(issue.Barcode, term) || ContainsIgnoreCase(issue.Isbn, term) || ContainsIgnoreCase(issue.ValidIsbn, term))
                {
                    score += 18;
                }

                if (ContainsIgnoreCase(issue.PublicationDate, term) || ContainsIgnoreCase(issue.OnSaleDate, term) || ContainsIgnoreCase(issue.KeyDate, term))
                {
                    score += 10;
                }

                if (ContainsIgnoreCase(issueText, term))
                {
                    score += 6;
                }

                if (ContainsIgnoreCase(issue.Series.Name, term) || ContainsIgnoreCase(issue.Series.SortName, term))
                {
                    score += 14;
                }

                if (ContainsIgnoreCase(seriesText, term))
                {
                    score += 5;
                }

                if (ContainsIgnoreCase(issue.Series.Publisher.Name, term))
                {
                    score += 12;
                }

                if (ContainsIgnoreCase(publisherText, term))
                {
                    score += 4;
                }

                if (RegexMatch(issueText, term))
                {
                    score += 3;
                }

                if (RegexMatch(seriesText, term) || RegexMatch(publisherText, term))
                {
                    score += 2;
                }
            }

            return score;
        }

        private static string BuildIssueSearchText(GcdIssue issue)
        {
            return string.Join(' ',
                issue.Number,
                issue.Volume,
                issue.PublicationDate,
                issue.KeyDate ?? string.Empty,
                issue.Price,
                issue.IndiciaFrequency,
                issue.Editing,
                issue.Notes,
                issue.Isbn,
                issue.ValidIsbn,
                issue.VariantName,
                issue.Barcode,
                issue.Title,
                issue.OnSaleDate,
                issue.Rating,
                issue.IndiciaPrinterSourcedBy,
                issue.SortCode.ToString(),
                issue.SeriesId.ToString(),
                issue.BrandId?.ToString() ?? string.Empty,
                issue.IndiciaPublisherId?.ToString() ?? string.Empty,
                issue.PageCount?.ToString() ?? string.Empty,
                issue.VariantOfId?.ToString() ?? string.Empty
            );
        }

        private static string BuildSeriesSearchText(GcdSeries series)
        {
            return string.Join(' ',
                series.Name,
                series.SortName,
                series.Format,
                series.PublicationDates,
                series.TrackingNotes,
                series.Notes,
                series.Color,
                series.Dimensions,
                series.PaperStock,
                series.Binding,
                series.PublishingFormat,
                series.YearBegan.ToString(),
                series.YearEnded?.ToString() ?? string.Empty,
                series.IssueCount.ToString()
            );
        }

        private static string BuildPublisherSearchText(GcdPublisher publisher)
        {
            return string.Join(' ',
                publisher.Name,
                publisher.Notes,
                publisher.Url,
                publisher.YearBegan?.ToString() ?? string.Empty,
                publisher.YearEnded?.ToString() ?? string.Empty,
                publisher.YearOverallBegan?.ToString() ?? string.Empty,
                publisher.YearOverallEnded?.ToString() ?? string.Empty
            );
        }

        private static bool ContainsIgnoreCase(string? source, string term)
        {
            return source?.Contains(term, StringComparison.OrdinalIgnoreCase) is true;
        }

        private static bool RegexMatch(string source, string pattern)
        {
            try
            {
                return Regex.IsMatch(source, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
            catch (ArgumentException)
            {
                return Regex.IsMatch(source, Regex.Escape(pattern), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
        }
    }
}
