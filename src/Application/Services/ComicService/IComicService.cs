using Domain.Entities;

namespace Application.Services.ComicService
{
    public interface IComicService
    {
        string GetComicFirstPage(string comicsPath);
        Task<string> SaveComicInfo(ComicInfo comicInfo, string comicsPath);
        ComicInfo CreateComicInfo(GcdIssue issue);
    }
}
