using Domain.Entities;
using System;
using System.Globalization;
using System.Linq;

namespace Application.Services.ComicService
{
    public class ComicService : IComicService
    {
        public ComicService()
        {
        }

        public ComicInfo CreateComicInfo(GcdIssue issue)
        {
            if (issue is null)
            {
                throw new ArgumentNullException(nameof(issue));
            }

            (int year, int month, int day) = GetDateParts(issue);
            int seriesCount = issue.Series is not null && issue.Series.IssueCount > 0 ? issue.Series.IssueCount : -1;
            int alternateCount = issue.VariantOf?.Series is not null && issue.VariantOf.Series.IssueCount > 0 ? issue.VariantOf.Series.IssueCount : -1;
            int volume = ParsePositiveInteger(issue.Volume);
            int pageCount = issue.PageCount ?? issue.GcdStories.Where(story => story.PageCount.HasValue).Sum(story => story.PageCount!.Value);

            string writer = GetCredits(issue, "script");
            string penciller = GetCredits(issue, "pencils");
            string inker = GetCredits(issue, "inks");
            string colorist = GetCredits(issue, "colors");
            string letterer = GetCredits(issue, "letters");
            string coverArtist = GetCredits(issue, "painting");
            string editor = GetCredits(issue, "editing");
            string genre = JoinDistinct(issue.GcdStories.Select(story => story.Genre));
            string characters = JoinDistinct(issue.GcdStories.Select(story => story.Characters));
            string storyArc = JoinDistinct(issue.GcdStories.Select(story => story.Feature));
            string summaryFromStories = JoinDistinct(issue.GcdStories.Select(story => story.Synopsis));

            ComicInfo comicInfo = new ComicInfo
            {
                Title = issue.NoTitle == 0 ? issue.Title : string.Empty,
                Series = issue.Series?.Name ?? string.Empty,
                Number = issue.Number,
                Count = seriesCount,
                Volume = volume,
                AlternateSeries = issue.VariantOf?.Series?.Name ?? string.Empty,
                AlternateNumber = issue.VariantOf?.Number ?? string.Empty,
                AlternateCount = alternateCount,
                Summary = !string.IsNullOrWhiteSpace(summaryFromStories) ? summaryFromStories : (issue.NoTitle == 0 ? issue.Title : string.Empty),
                Notes = JoinDistinct(new[] { issue.Notes, issue.Editing }),
                Year = year,
                Month = month,
                Day = day,
                Writer = writer,
                Penciller = penciller,
                Inker = inker,
                Colorist = colorist,
                Letterer = letterer,
                CoverArtist = coverArtist,
                Editor = editor,
                Publisher = issue.IndiciaPublisher?.Name ?? issue.Series?.Publisher?.Name ?? string.Empty,
                Imprint = issue.IndiciaPublisher?.Name ?? string.Empty,
                Genre = genre,
                Web = issue.Series?.Publisher?.Url ?? issue.IndiciaPublisher?.Url ?? string.Empty,
                PageCount = pageCount,
                LanguageISO = issue.Series?.Language?.Code ?? string.Empty,
                Format = issue.Series?.Format ?? issue.Series?.PublishingFormat ?? string.Empty,
                BlackAndWhite = GetBlackAndWhite(issue),
                Manga = GetManga(issue, genre),
                Characters = characters,
                Teams = string.Empty,
                Locations = string.Empty,
                ScanInformation = issue.IndiciaPrinterSourcedBy,
                StoryArc = storyArc,
                SeriesGroup = issue.Series?.PublicationType?.Name ?? string.Empty,
                AgeRating = GetAgeRating(issue.Rating),
                Pages = Array.Empty<ComicPageInfo>(),
                CommunityRating = 0,
                CommunityRatingSpecified = false,
                MainCharacterOrTeam = GetMainCharacterOrTeam(characters, string.Empty),
                Review = string.Empty
            };

            return comicInfo;
        }

        private string GetCredits(GcdIssue issue, params string[] acceptedTypes)
        {
            var test = issue.GcdStories.SelectMany(story => story.GcdStoryCredits).ToList().Select(c => c.CreditType.Name).ToList();

            IEnumerable<string> storyCredits = issue.GcdStories.SelectMany(story => story.GcdStoryCredits)
                .Where(credit => credit.CreditType is not null && acceptedTypes.Any(acceptedType => credit.CreditType.Name.Contains(acceptedType, StringComparison.OrdinalIgnoreCase)))
                .Select(GetStoryCreditName);

            IEnumerable<string> issueCredits = issue.GcdIssueCredits
                .Where(credit => credit.CreditType is not null && acceptedTypes.Any(acceptedType => credit.CreditType.Name.Contains(acceptedType, StringComparison.OrdinalIgnoreCase)))
                .Select(GetCreditName);

            return JoinDistinct(storyCredits.Concat(issueCredits).Distinct());
        }

        private string GetCreditName(GcdIssueCredit credit)
        {
            if (!string.IsNullOrWhiteSpace(credit.Creator.Creator.GcdOfficialName))
            {
                return credit.Creator.Creator.GcdOfficialName;
            }

            if (!string.IsNullOrWhiteSpace(credit.CreditedAs))
            {
                return credit.CreditedAs;
            }

            if (!string.IsNullOrWhiteSpace(credit.CreditName))
            {
                return credit.CreditName;
            }

            return credit.Creator?.Name ?? string.Empty;
        }

        private string GetStoryCreditName(GcdStoryCredit credit)
        {
            if (!string.IsNullOrWhiteSpace(credit.Creator.Creator.GcdOfficialName))
            {
                return credit.Creator.Creator.GcdOfficialName;
            }

            if (!string.IsNullOrWhiteSpace(credit.CreditedAs))
            {
                return credit.CreditedAs;
            }

            if (!string.IsNullOrWhiteSpace(credit.CreditName))
            {
                return credit.CreditName;
            }

            return credit.Creator?.Name ?? string.Empty;
        }

        private string JoinDistinct(System.Collections.Generic.IEnumerable<string?> values)
        {
            string[] normalized = values
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(value => value!.Trim())
                .Where(value => value.Length > 0)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            return string.Join(", ", normalized);
        }

        private int ParsePositiveInteger(string value)
        {
            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed) && parsed > 0)
            {
                return parsed;
            }

            return -1;
        }

        private (int Year, int Month, int Day) GetDateParts(GcdIssue issue)
        {
            (int Year, int Month, int Day) keyDate = ParseDate(issue.KeyDate);
            if (keyDate.Year != -1)
            {
                return keyDate;
            }

            (int Year, int Month, int Day) onSaleDate = ParseDate(issue.OnSaleDate);
            if (onSaleDate.Year != -1)
            {
                return onSaleDate;
            }

            return ParseDate(issue.PublicationDate);
        }

        private (int Year, int Month, int Day) ParseDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return (-1, -1, -1);
            }

            string[] parts = value.Trim().Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length >= 1 && int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int year) && year > 0)
            {
                int month = -1;
                int day = -1;

                if (parts.Length >= 2 && int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsedMonth) && parsedMonth is >= 1 and <= 12)
                {
                    month = parsedMonth;
                }

                if (parts.Length >= 3 && int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsedDay) && parsedDay is >= 1 and <= 31)
                {
                    day = parsedDay;
                }

                return (year, month, day);
            }

            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime parsedDate))
            {
                return (parsedDate.Year, parsedDate.Month,(parsedDate.Day));
            }

            return (-1, -1, -1);
        }

        private YesNo GetBlackAndWhite(GcdIssue issue)
        {
            string normalized = Normalize(issue.Series?.Color ?? string.Empty);
            if (normalized.Contains("BLACKANDWHITE", StringComparison.Ordinal) || normalized.Contains("BW", StringComparison.Ordinal))
            {
                return YesNo.Yes;
            }

            if (normalized.Contains("COLOR", StringComparison.Ordinal))
            {
                return YesNo.No;
            }

            return YesNo.Unknown;
        }

        private Manga GetManga(GcdIssue issue, string genre)
        {
            string source = JoinDistinct(new[] { issue.Series?.Format, issue.Series?.PublishingFormat, issue.Series?.PublicationType?.Name, genre });
            string normalized = Normalize(source);
            if (normalized.Contains("MANGA", StringComparison.Ordinal))
            {
                return Manga.YesAndRightToLeft;
            }

            return Manga.Unknown;
        }

        private AgeRating GetAgeRating(string? rating)
        {
            string normalized = Normalize(rating ?? string.Empty);

            if (normalized == "G")
            {
                return AgeRating.G;
            }

            if (normalized == "PG")
            {
                return AgeRating.PG;
            }

            if (normalized == "M")
            {
                return AgeRating.M;
            }

            if (normalized == "TEEN" || normalized == "T")
            {
                return AgeRating.Teen;
            }

            if (normalized == "EVERYONE" || normalized == "E" || normalized == "ALLAGES")
            {
                return AgeRating.Everyone;
            }

            return AgeRating.Unknown;
        }

        private string Normalize(string value)
        {
            char[] chars = value.ToUpperInvariant().Where(character => char.IsLetterOrDigit(character)).ToArray();
            return new string(chars);
        }

        private string GetMainCharacterOrTeam(string characters, string teams)
        {
            if (!string.IsNullOrWhiteSpace(teams))
            {
                string[] teamValues = teams.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (teamValues.Length > 0)
                {
                    return teamValues[0];
                }
            }

            if (!string.IsNullOrWhiteSpace(characters))
            {
                string[] characterValues = characters.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (characterValues.Length > 0)
                {
                    return characterValues[0];
                }
            }

            return string.Empty;
        }
    }
}
