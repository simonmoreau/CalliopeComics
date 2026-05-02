using Application.Issues.Queries.GetIssueDetailsQuery;
using Application.Issues.Queries.SearchIssuesQuery;
using Application.Services.Gemini;
using Domain.DTO;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services.ComicService
{
    public sealed class ComicsProcessorService : BackgroundService
    {
        private readonly ILogger<ComicsProcessorService> _logger;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IComicService _comicService;
        private readonly IGeminiClient _geminiClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ComicsProcessorService(
            ILogger<ComicsProcessorService> logger,
            IOptions<ApplicationSettings> applicationSettings,
            IComicService comicService,
            IGeminiClient geminiClient,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _applicationSettings = applicationSettings.Value;
            _comicService = comicService;
            _geminiClient = geminiClient;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Comics processor background service started.");

            return;
            try
            {
                string storagePath = _applicationSettings.StoragePath;
                if (string.IsNullOrWhiteSpace(storagePath))
                {
                    _logger.LogWarning("Comics processor skipped because storage path is not configured.");
                    return;
                }

                if (!Directory.Exists(storagePath))
                {
                    _logger.LogWarning("Comics processor skipped because storage path '{StoragePath}' does not exist.", storagePath);
                    return;
                }

                string processedStoragePath = string.IsNullOrWhiteSpace(_applicationSettings.ProcessedStoragePath)
                    ? Path.Combine(storagePath, "processed")
                    : _applicationSettings.ProcessedStoragePath;

                if (!Directory.Exists(processedStoragePath))
                {
                    Directory.CreateDirectory(processedStoragePath);
                }

                string[] archives = Directory.EnumerateFiles(storagePath, "*", SearchOption.AllDirectories)
                    .Where(IsSupportedArchive)
                    .ToArray();

                using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                foreach (string archivePath in archives)
                {
                    if (stoppingToken.IsCancellationRequested)
                    {
                        break;
                    }

                    if (archivePath.StartsWith(processedStoragePath, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string? firstPagePath = null;

                    try
                    {
                        firstPagePath = _comicService.GetComicFirstPage(archivePath);
                        string comicBookName = Path.GetFileName(archivePath);
                        string directoryName = Path.GetDirectoryName(archivePath) ?? string.Empty;
                        string combicBookPath = Path.Combine(directoryName, comicBookName);
                        string searchTerm = await _geminiClient.AnalyseImageAsync(firstPagePath, combicBookPath);

                        _logger.LogInformation("Generated search term for archive '{ArchivePath}': '{SearchTerm}'.", archivePath, searchTerm);

                        if (string.IsNullOrWhiteSpace(searchTerm))
                        {
                            _logger.LogWarning("No search term generated for archive '{ArchivePath}'.", archivePath);
                            continue;
                        }

                        SearchIssuesQuery query = new SearchIssuesQuery(searchTerm);
                        List<IssueDto> issues = await mediator.Send(query, stoppingToken);

                        if (issues.Count == 0)
                        {
                            _logger.LogWarning("No issue match found for '{searchTerm}''{ArchivePath}'.", searchTerm, archivePath);
                            continue;
                        }

                        GetIssueDetailsQuery detailsQuery = new GetIssueDetailsQuery(issues[0].Id);
                        GcdIssue issue = await mediator.Send(detailsQuery, stoppingToken);
                        ComicInfo comicInfo = _comicService.CreateComicInfo(issue);

                        
                        string savedArchivePath = _comicService.SaveComicInfo(comicInfo, archivePath);

                        string copiedArchivePath = Path.Combine(processedStoragePath, Path.GetFileName(savedArchivePath));

                        File.Move(savedArchivePath, copiedArchivePath, true);

                        _logger.LogInformation(
                            "Processed archive '{ArchivePath}'. SearchTerm='{SearchTerm}'. Matches={MatchCount}. Saved='{SavedArchivePath}'.",
                            archivePath,
                            searchTerm,
                            issues.Count,
                            copiedArchivePath);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to process archive '{ArchivePath}'.", archivePath);
                    }
                    finally
                    {
                        if (!string.IsNullOrWhiteSpace(firstPagePath) && File.Exists(firstPagePath))
                        {
                            try
                            {
                                File.Delete(firstPagePath);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning(ex, "Unable to delete temporary image '{ImagePath}'.", firstPagePath);
                            }
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Comics processor background service cancellation requested.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in comics processor background service.");
            }

            _logger.LogInformation("Comics processor background service stopped.");
        }

        private static bool IsSupportedArchive(string archivePath)
        {
            string extension = Path.GetExtension(archivePath).ToLowerInvariant();
            return extension is ".cbz" or ".cbr";
        }
    }
}
