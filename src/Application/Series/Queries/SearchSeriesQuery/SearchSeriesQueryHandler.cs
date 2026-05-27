using Application.Common.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Application.Series.Queries.SearchSeriesQuery
{
    public class SearchSeriesQueryHandler : IAuthenticatedRequestHandler<SearchSeriesQuery, List<SeriesDto>>
    {
        private readonly IGcdDbContext _context;
        private readonly ILogger<SearchSeriesQueryHandler> _logger;

        public SearchSeriesQueryHandler(IGcdDbContext context, ILogger<SearchSeriesQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<SeriesDto>> Handle(SearchSeriesQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                return [];
            }

            string[] caractersToRemoves = ["<", ">", ",", "\"", "'", ";", ":", "&", "#", "(", ")", "[", "]", "{", "}"];
            string sanitizedSearchTerm = caractersToRemoves.Aggregate(request.SearchTerm, (current, characterToRemove) => current.Replace(characterToRemove, " "));
            string[] terms = sanitizedSearchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string[] normalizedTerms = terms
                .Select(NormalizeSearchTerm)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();

            IQueryable<GcdSeries> seriesQuery = _context.GcdSeries
                .Include(series => series.Publisher)
                .Include(series => series.Language);

            foreach (string term in normalizedTerms)
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    continue;
                }

                seriesQuery = seriesQuery.Where(series =>
                    EF.Functions.Like(series.Name, $"%{term}%") ||
                    EF.Functions.Like(series.SortName, $"%{term}%") ||
                    EF.Functions.Like(series.PublicationDates, $"%{term}%") ||
                    EF.Functions.Like(series.YearBegan.ToString(), $"%{term}%") ||
                    EF.Functions.Like(series.TrackingNotes, $"%{term}%") ||
                    EF.Functions.Like(series.Notes, $"%{term}%") ||
                    EF.Functions.Like(series.Publisher.Name, $"%{term}%")
                );
            }

            if (request.Date.HasValue)
            {
                int year = request.Date.Value.Year;
                seriesQuery = seriesQuery.Where(series =>
                    series.YearBegan <= year &&
                    (series.YearEnded == null || year <= series.YearEnded));
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            List<GcdSeries> seriesResults = await seriesQuery
                .OrderBy(series => series.Created)
                .Take(100)
                .ToListAsync(cancellationToken);

            stopwatch.Stop();
            _logger.LogInformation("SearchSeries query executed in {ElapsedMilliseconds} ms and returned {SeriesCount} series.", stopwatch.ElapsedMilliseconds, seriesResults.Count);

            if (seriesResults.Count == 100)
            {
                IQueryable<GcdSeries> localizedQuery = seriesQuery
                    .Where(series => series.Language.Code == "en" || series.Language.Code == "fr");

                seriesResults = await localizedQuery
                    .OrderBy(series => series.Created)
                    .Take(100)
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("localized SearchSeries query executed and returned {SeriesCount} series.", seriesResults.Count);
            }

            List<SeriesDto> results = seriesResults
                .Select(series => new
                {
                    Series = series,
                    Score = CalculateWeightedScore(series, normalizedTerms)
                })
                .Where(result => result.Score > 0)
                .OrderByDescending(result => result.Score)
                .ThenByDescending(result => result.Series.Modified)
                .Select(result => new SeriesDto(result.Series))
                .ToList();

            return results;
        }

        private string NormalizeSearchTerm(string term)
        {
            if (int.TryParse(term, out int number))
            {
                return number.ToString();
            }

            if (term == "-")
            {
                return "";
            }

            return term;
        }

        private int CalculateWeightedScore(GcdSeries series, string[] terms)
        {
            int score = 0;
            string seriesText = BuildSeriesSearchText(series);
            string publisherText = BuildPublisherSearchText(series.Publisher);

            foreach (string term in terms)
            {
                if (ContainsIgnoreCase(series.Name, term) || ContainsIgnoreCase(series.SortName, term))
                {
                    score += 20;
                }

                if (ContainsIgnoreCase(series.PublicationDates, term) || ContainsIgnoreCase(series.Format, term))
                {
                    score += 12;
                }

                if (ContainsIgnoreCase(series.Publisher.Name, term))
                {
                    score += 14;
                }

                if (ContainsIgnoreCase(series.Language.Code, term))
                {
                    score += 10;
                }

                if (ContainsIgnoreCase(seriesText, term))
                {
                    score += 6;
                }

                if (ContainsIgnoreCase(publisherText, term))
                {
                    score += 4;
                }

                if (RegexMatch(seriesText, term))
                {
                    score += 3;
                }

                if (RegexMatch(publisherText, term))
                {
                    score += 2;
                }
            }

            return score;
        }

        private string BuildSeriesSearchText(GcdSeries series)
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

        private string BuildPublisherSearchText(GcdPublisher publisher)
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

        private bool ContainsIgnoreCase(string? source, string term)
        {
            return source?.Contains(term, StringComparison.OrdinalIgnoreCase) is true;
        }

        private bool RegexMatch(string source, string pattern)
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
