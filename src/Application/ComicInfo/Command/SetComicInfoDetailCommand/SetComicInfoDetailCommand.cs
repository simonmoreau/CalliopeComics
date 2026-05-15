using Application.Interfaces;
using Application.Services.ComicService;
using Domain.Entities;

namespace Application.ComicInfo.Command.SetComicInfoDetailCommand
{
    // Command to refresh/update ComicInfo details for a specific issue inside a comic archive
    public class SetComicInfoDetailCommand : AuthenticatedRequest<bool>
    {
        public int IssueId { get; }
        public string ComicsPath { get; }
        public string? SeriesGroup { get; }

        public SetComicInfoDetailCommand(int issueId, string comicsPath, string? seriesGroup = null)
        {
            IssueId = issueId;
            ComicsPath = comicsPath;
            SeriesGroup = seriesGroup;
        }
    }
}
