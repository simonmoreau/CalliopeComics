using Domain.Entities;

namespace Application.Services.ComicService
{
    public interface IComicService
    {
        string GetComicFirstPage(string comicsPath);
        ComicInfo CreateComicInfo(GcdIssue issue, string? seriesGroup = null);
        Task<string> SaveComicInfo(ComicInfo comicInfo, string comicsPath, CancellationToken cancellationToken = default);
    }
}
