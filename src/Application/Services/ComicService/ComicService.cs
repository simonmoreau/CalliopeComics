using Application.Services.GrandComicDatabase;
using Domain.Entities;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Application.Services.ComicService
{
    public class ComicService : IComicService
    {
        public ComicService()
        {
        }

        public string GetComicFirstPage(string comicsPath)
        {
            if (string.IsNullOrWhiteSpace(comicsPath))
            {
                throw new ArgumentException("The comic archive path is required.", nameof(comicsPath));
            }

            if (!File.Exists(comicsPath))
            {
                throw new FileNotFoundException("The comic archive file was not found.", comicsPath);
            }

            string extension = Path.GetExtension(comicsPath);
            string normalizedExtension = extension.ToLowerInvariant();
            if (normalizedExtension is not ".cbz" and not ".cbr" and not ".crz" and not ".crb")
            {
                throw new NotSupportedException($"Unsupported comic archive extension '{extension}'.");
            }

            string extractionDirectoryPath = Path.Combine(Path.GetTempPath(), $"calliope_extract_{Guid.NewGuid():N}");
            string outputDirectoryPath = Path.Combine(Path.GetTempPath(), $"calliope_firstpage_{Guid.NewGuid():N}");

            Directory.CreateDirectory(extractionDirectoryPath);
            Directory.CreateDirectory(outputDirectoryPath);

            bool success = false;
            try
            {
                using FileStream archiveStream = File.OpenRead(comicsPath);
                using IReader archive = ReaderFactory.Open(archiveStream);

                while (archive.MoveToNextEntry())
                {
                    IEntry entry = archive.Entry;
                    if (entry == null) continue;
                    if (entry.IsDirectory) continue;
                    if (entry.Key == null) continue;

                    if (!IsSupportedImageFile(entry.Key)) continue;

                    // Extract to directory (preserves path structure)
                    archive.WriteEntryToDirectory(extractionDirectoryPath, new ExtractionOptions()
                    {
                        ExtractFullPath = true,  // Recreates subdirectories
                        Overwrite = true         // Overwrite if exists
                    });
                    string extractedFilePath = Path.Combine(extractionDirectoryPath, entry.Key);
                    success = true;
                    return extractedFilePath;
                }

                throw new InvalidOperationException("No image file was found in the comic archive.");
            }
            finally
            {
                if (!success && Directory.Exists(extractionDirectoryPath))
                {
                    Directory.Delete(extractionDirectoryPath, true);
                }
            }
        }

        private bool IsSupportedImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension is ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".webp" or ".tif" or ".tiff";
        }

        public ComicInfo CreateComicInfo(GcdIssue issue, string? seriesGroup = null)
        {
            if (issue is null)
            {
                throw new ArgumentNullException(nameof(issue));
            }

            (int year, int month, int day) = GetDateParts(issue);
            int seriesCount = issue.Series is not null && issue.Series.IssueCount > 0 ? issue.Series.IssueCount : -1;
            int alternateCount = issue.VariantOf?.Series is not null && issue.VariantOf.Series.IssueCount > 0 ? issue.VariantOf.Series.IssueCount : -1;
            int volume = issue.Series?.YearBegan > 0 ? issue.Series.YearBegan : year;
            int pageCount = issue.PageCount ?? issue.GcdStories.Where(story => story.PageCount.HasValue).Sum(story => story.PageCount!.Value);

            string writer = GetCredits(issue, "script");
            string penciller = GetCredits(issue, "pencils");
            string inker = GetCredits(issue, "inks");
            string colorist = GetCredits(issue, "colors");
            string letterer = GetCredits(issue, "letters");
            string coverArtist = GetCredits(issue, "painting");
            string editor = GetCredits(issue, "editing");
            string genre = JoinDistinct(issue.GcdStories.Select(story => story.Genre));
            string characters = GetCaracters(issue);
            string teams = GetTeams(issue);
            string storyArc = JoinDistinct(issue.GcdStories.Select(story => story.Feature));
            string summaryFromStories = JoinDistinct(issue.GcdStories.SelectMany(story => new[] { story.Synopsis, story.FirstLine, story.Notes }));
            string web = issue.Series?.Publisher?.Url ?? issue.IndiciaPublisher?.Url ?? string.Empty;
            string gcdWeb = $"https://www.comics.org/issue/{issue.Id}/";
            string issueNuber = issue.Number;
            

            if (issue.Number.Contains("nn"))
            {
                issueNuber = "1";
            }

            ComicInfo comicInfo = new ComicInfo
            {
                Title = issue.NoTitle == 0 ? issue.Title : string.Empty,
                Series = issue.Series?.Name ?? string.Empty,
                Number = issueNuber,
                Count = seriesCount,
                Volume = volume,
                AlternateSeries = issue.VariantOf?.Series?.Name ?? string.Empty,
                AlternateNumber = issue.VariantOf?.Number ?? string.Empty,
                AlternateCount = alternateCount,
                Summary = !string.IsNullOrWhiteSpace(summaryFromStories) ? summaryFromStories : (issue.NoTitle == 0 ? issue.Title : string.Empty),
                Notes = issue.Notes,
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
                Publisher = issue.Series?.Publisher?.Name ?? issue.IndiciaPublisher?.Name ?? string.Empty,
                Imprint = issue.Brand?.Name ?? string.Empty,
                Genre = genre,
                Web = JoinDistinct(new[] { gcdWeb }),
                PageCount = pageCount,
                LanguageISO = issue.Series?.Language?.Code ?? string.Empty,
                Format = issue.Series?.PublishingFormat ?? issue.Series?.Format ?? issue.Series?.PublicationType?.Name ?? string.Empty,
                BlackAndWhite = GetBlackAndWhite(issue),
                Manga = GetManga(issue, genre),
                Characters = characters,
                Teams = teams,
                Locations = string.Empty,
                ScanInformation = issue.IndiciaPrinterSourcedBy,
                StoryArc = storyArc,
                SeriesGroup = seriesGroup ?? string.Empty,
                AgeRating = GetAgeRating(issue.Rating),
                Pages = Array.Empty<ComicPageInfo>(),
                CommunityRating = 0,
                CommunityRatingSpecified = false,
                GTIN = !string.IsNullOrWhiteSpace(issue.Isbn) ? issue.Isbn : (!string.IsNullOrWhiteSpace(issue.Barcode) ? issue.Barcode : string.Empty),
                MainCharacterOrTeam = GetMainCharacterOrTeam(issue, teams),
                Review = string.Empty
            };

            return comicInfo;
        }

        private string GetCredits(GcdIssue issue, params string[] acceptedTypes)
        {
            IEnumerable<string> storyCredits = issue.GcdStories.SelectMany(story => story.GcdStoryCredits)
                .Where(credit => credit.CreditType is not null && acceptedTypes.Any(acceptedType => credit.CreditType.Name.Contains(acceptedType, StringComparison.OrdinalIgnoreCase)))
                .Select(GetStoryCreditName);

            IEnumerable<string> issueCredits = issue.GcdIssueCredits
                .Where(credit => credit.CreditType is not null && acceptedTypes.Any(acceptedType => credit.CreditType.Name.Contains(acceptedType, StringComparison.OrdinalIgnoreCase)))
                .Select(GetCreditName);

            return JoinDistinct(storyCredits.Concat(issueCredits).Distinct());
        }

        private string GetCaracters(GcdIssue issue)
        {
            var names = new List<string>();

            if (issue?.GcdStories == null) return string.Empty;

            foreach (GcdStory story in issue.GcdStories)
            {
                if (story?.GcdStoryCharacters == null) continue;
                foreach (GcdStoryCharacter? storyCharacter in story.GcdStoryCharacters)
                {
                    // Primary: character name
                    var name = storyCharacter?.Character?.Name;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        names.Add(name);
                    }
                }

                // Also parse the story.Characters text field for additional names
                if (!string.IsNullOrWhiteSpace(story.Characters))
                {
                    string[] textChars = story.Characters.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    foreach (string textChar in textChars)
                    {
                        // Remove parenthetical notes like " (villain)", " (death)" etc.
                        string cleanName = textChar.Split('(', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0].Trim();
                        // Remove bracket aliases like " [Reed Richards]"
                        cleanName = cleanName.Split('[', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0].Trim();
                        if (!string.IsNullOrWhiteSpace(cleanName))
                        {
                            names.Add(cleanName);
                        }
                    }
                }
            }

            return JoinDistinct(names);
        }

        private string GetTeams(GcdIssue issue)
        {
            var names = new List<string>();

            if (issue?.GcdStories == null) return string.Empty;

            foreach (GcdStory story in issue.GcdStories)
            {
                if (story?.GcdGroupCharacters == null) continue;
                foreach (GcdGroupCharacter? groupCharacter in story.GcdGroupCharacters)
                {
                    // Primary: character name
                    var name = groupCharacter?.GroupName?.Name;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        names.Add(name);
                    }
                }
            }

            return JoinDistinct(names);
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
            if (normalized.Contains("BLACKANDWHITE", StringComparison.Ordinal) || 
                normalized.Contains("BW", StringComparison.Ordinal) ||
                normalized.Contains("MONOCHROME", StringComparison.Ordinal))
            {
                return YesNo.Yes;
            }

            if (normalized.Contains("COLOR", StringComparison.Ordinal) ||
                normalized.Contains("COLOUR", StringComparison.Ordinal) ||
                normalized.Contains("COLORI", StringComparison.Ordinal) ||
                normalized.Contains("FARBIG", StringComparison.Ordinal) ||
                normalized.Contains("COULEUR", StringComparison.Ordinal) ||
                normalized.Contains("KLEUR", StringComparison.Ordinal))
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

            if (normalized == "TEEN" || normalized == "T" || normalized == "RATEDT" || normalized == "RATEDT+")
            {
                return AgeRating.Teen;
            }

            if (normalized == "EVERYONE" || normalized == "E" || normalized == "ALLAGES")
            {
                return AgeRating.Everyone;
            }

            if (normalized == "EVERYONE10" || normalized == "E10" || normalized == "E10+")
            {
                return AgeRating.Everyone10;
            }

            if (normalized == "MATURE17" || normalized == "M17" || normalized == "M17+")
            {
                return AgeRating.Mature17;
            }

            if (normalized == "RATEDA" || normalized == "ADULTSONLY18" || normalized == "A18" || normalized == "A18+")
            {
                return AgeRating.AdultsOnly18;
            }

            if (normalized == "MA15" || normalized == "MA15+")
            {
                return AgeRating.MA15;
            }

            if (normalized == "R18" || normalized == "R18+")
            {
                return AgeRating.R18;
            }

            if (normalized == "X18" || normalized == "X18+")
            {
                return AgeRating.X18;
            }

            return AgeRating.Unknown;
        }

        private string Normalize(string value)
        {
            char[] chars = value.ToUpperInvariant().Where(character => char.IsLetterOrDigit(character)).ToArray();
            return new string(chars);
        }

        private string GetMainCharacterOrTeam(GcdIssue issue, string teams)
        {
            if (!string.IsNullOrWhiteSpace(teams))
            {
                string[] teamValues = teams.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (teamValues.Length > 0)
                {
                    return teamValues[0];
                }
            }

            List<string> names = new List<string>();

            foreach (GcdStory story in issue.GcdStories)
            {
                if (story?.GcdStoryCharacters == null) continue;
                foreach (GcdStoryCharacter? storyCharacter in story.GcdStoryCharacters)
                {
                    if (storyCharacter?.Role?.Name != "featured") continue;
                    // Primary: character name
                    var name = storyCharacter?.Character?.Name;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        names.Add(name);
                    }
                }
            }

            return JoinDistinct(names);
        }
    }
}
