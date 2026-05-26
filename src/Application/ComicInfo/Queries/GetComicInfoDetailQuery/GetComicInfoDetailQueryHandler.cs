using Application.Common.Interfaces;
using SharpCompress.Readers;
using System.Reflection;
using System.Xml.Serialization;

namespace Application.ComicInfo.Queries.GetComicInfoDetailQuery
{
    public class GetComicInfoDetailQueryHandler : IAuthenticatedRequestHandler<GetComicInfoDetailQuery, string?>
    {
        public Task<string?> Handle(GetComicInfoDetailQuery request, CancellationToken cancellationToken)
        {
            if (!File.Exists(request.FilePath))
                return Task.FromResult<string?>(null);

            using (FileStream archiveStream = File.OpenRead(request.FilePath))
            {
                using (IReader archive = ReaderFactory.Open(archiveStream))
                {
                    while (archive.MoveToNextEntry())
                    {
                        if (!string.Equals(archive.Entry.Key, "ComicInfo.xml", StringComparison.OrdinalIgnoreCase))
                            continue;

                        using MemoryStream memStream = new();
                        archive.WriteEntryTo(memStream);
                        memStream.Position = 0;

                        XmlSerializer serializer = new(typeof(Services.ComicService.ComicInfo));
                        Services.ComicService.ComicInfo? comicInfo = (Services.ComicService.ComicInfo?)serializer.Deserialize(memStream);
                        if (comicInfo == null)
                            return Task.FromResult<string?>(null);

                        PropertyInfo? property = typeof(Services.ComicService.ComicInfo).GetProperty(request.PropertyName);
                        if (property == null)
                            return Task.FromResult<string?>(null);

                        object? value = property.GetValue(comicInfo);
                        return Task.FromResult(value?.ToString());
                    }
                }
            }

            return Task.FromResult<string?>(null);
        }
    }
}
