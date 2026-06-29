using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Services.ComicService;
using Microsoft.Extensions.Logging;
using SharpCompress.Readers;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

namespace Application.ComicInfo.Command.SetComicInfoPropertyCommand
{
    public class SetComicInfoPropertyCommandHandler : IAuthenticatedRequestHandler<SetComicInfoPropertyCommand, bool>
    {
        private readonly IComicService _comicService;
        private readonly ILogger<SetComicInfoPropertyCommandHandler> _logger;

        public SetComicInfoPropertyCommandHandler(IComicService comicService, ILogger<SetComicInfoPropertyCommandHandler> logger)
        {
            _comicService = comicService;
            _logger = logger;
        }

        public async Task<bool> Handle(SetComicInfoPropertyCommand request, CancellationToken cancellationToken)
        {
            Services.ComicService.ComicInfo comicInfo = ReadComicInfoFromArchive(request.FilePath);

            PropertyInfo? property = typeof(Services.ComicService.ComicInfo).GetProperty(request.PropertyName);
            if (property == null)
            {
                throw new NotFoundException($"Property '{request.PropertyName}'", typeof(Services.ComicService.ComicInfo).Name);
            }

            if (property.PropertyType == typeof(ComicPageInfo[]))
            {
                throw new InvalidOperationException($"Cannot set property '{request.PropertyName}' of type '{property.PropertyType.Name}' from a string value.");
            }

            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
            object? convertedValue = converter.ConvertFromString(request.Value) ?? throw new InvalidOperationException($"Cannot convert value '{request.Value}' to type '{property.PropertyType.Name}'.");

            property.SetValue(comicInfo, convertedValue);

            await _comicService.SaveComicInfo(comicInfo, request.FilePath, cancellationToken);

            _logger.LogInformation("ComicInfo property '{PropertyName}' set to '{Value}' in {Path}", request.PropertyName, request.Value, request.FilePath);
            return true;
        }

        private static Services.ComicService.ComicInfo ReadComicInfoFromArchive(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new NotFoundException(nameof(filePath), filePath);
            }

            using FileStream archiveStream = File.OpenRead(filePath);
            using IReader reader = ReaderFactory.Open(archiveStream);

            while (reader.MoveToNextEntry())
            {
                if (!string.Equals(reader.Entry.Key, "ComicInfo.xml", StringComparison.OrdinalIgnoreCase))
                    continue;

                using MemoryStream memStream = new();
                reader.WriteEntryTo(memStream);
                memStream.Position = 0;

                XmlSerializer serializer = new(typeof(Services.ComicService.ComicInfo));
                Services.ComicService.ComicInfo? comicInfo = (Services.ComicService.ComicInfo?)serializer.Deserialize(memStream);
                if (comicInfo == null)
                {
                    throw new NotFoundException("ComicInfo.xml", filePath);
                }

                return comicInfo;
            }

            throw new NotFoundException("ComicInfo.xml", filePath);
        }
    }
}