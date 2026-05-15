using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Services.ComicService;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
            Application.Services.ComicService.ComicInfo comicInfo = _comicService.CreateComicInfo(issue);
            await SaveComicInfo(comicInfo, request.ComicsPath);

            _logger.LogInformation("ComicInfo updated for Issue {IssueId} in {Path}", request.IssueId, request.ComicsPath);
            return true;
        }


        private async Task<string> SaveComicInfo(Application.Services.ComicService.ComicInfo comicInfo, string comicsPath)
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

            XmlSerializer serializer = new XmlSerializer(typeof(Application.Services.ComicService.ComicInfo));
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
                    File.Move(temporaryArchivePath, resultPath, true);
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

    }
}
