using Application.Interfaces;

namespace Application.ComicInfo.Queries.GetComicInfoDetailQuery
{
    public class GetComicInfoDetailQuery : AuthenticatedRequest<string?>
    {
        public string FilePath { get; }
        public string PropertyName { get; }

        public GetComicInfoDetailQuery(string filePath, string propertyName)
        {
            FilePath = filePath;
            PropertyName = propertyName;
        }
    }
}
