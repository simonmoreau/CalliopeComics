using Domain.Entities;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
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

        public async Task<string> SaveComicInfo(ComicInfo comicInfo, string comicsPath)
        {
            if (comicInfo is null)
            {
                throw new ArgumentNullException(nameof(comicInfo));
            }

            if (string.IsNullOrWhiteSpace(comicsPath))
            {
                throw new ArgumentException("The comic archive path is required.", nameof(comicsPath));
            }

            if (!File.Exists(comicsPath))
            {
                throw new FileNotFoundException("The comic archive file was not found.", comicsPath);
            }

            string extension = Path.GetExtension(comicsPath).ToLowerInvariant();
            if (extension is not ".cbz" and not ".cbr" and not ".crz" and not ".crb")
            {
                throw new NotSupportedException($"Unsupported comic archive extension '{extension}'.");
            }

            if (!ArchiveFactory.IsArchive(comicsPath, out ArchiveType? detectedType) || detectedType is null)
            {
                throw new InvalidOperationException("Could not detect archive type from file content.");
            }

            bool isRarBasedArchive = detectedType == ArchiveType.Rar;
            string extractDirectoryPath = Path.Combine(Path.GetTempPath(), $"calliope_comicinfo_extract_{Guid.NewGuid():N}");
            string xmlFilePath = Path.Combine(extractDirectoryPath, "ComicInfo.xml");
            string temporaryArchivePath = Path.Combine(Path.GetTempPath(), $"calliope_comicinfo_archive_{Guid.NewGuid():N}.cbz");

            Directory.CreateDirectory(extractDirectoryPath);

            XmlSerializer serializer = new XmlSerializer(typeof(ComicInfo));
            using (FileStream xmlFileStream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(xmlFileStream, comicInfo);
            }

            string resultPath = comicsPath;
            try
            {
                if (isRarBasedArchive)
                {
                    using RarArchive archive = RarArchive.Open(comicsPath);
                    foreach (IArchiveEntry entry in archive.Entries.Where(archiveEntry => !archiveEntry.IsDirectory))
                    {
                        entry.WriteToDirectory(extractDirectoryPath, new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }

                    ZipFile.CreateFromDirectory(extractDirectoryPath, temporaryArchivePath);
                    resultPath = Path.ChangeExtension(comicsPath, ".cbz");
                    File.Move(temporaryArchivePath, resultPath,true);
                }
                else
                {
                    using (ZipArchive archive = ZipFile.Open(comicsPath, ZipArchiveMode.Update))
                    {
                        archive.Entries.Where(entry => string.Equals(entry.FullName, "ComicInfo.xml", StringComparison.OrdinalIgnoreCase)).ToList()
                            .ForEach(entry => entry.Delete());
                        await archive.CreateEntryFromFileAsync(xmlFilePath, Path.GetFileName(xmlFilePath));
                    }
                }

                return resultPath;
            }
            finally
            {
                if (Directory.Exists(extractDirectoryPath))
                {
                    Directory.Delete(extractDirectoryPath, true);
                }

                if (File.Exists(temporaryArchivePath))
                {
                    File.Delete(temporaryArchivePath);
                }
            }
        }

        private bool IsSupportedImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension is ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".webp" or ".tif" or ".tiff";
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
                Web = JoinDistinct(new[] { gcdWeb }),
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
