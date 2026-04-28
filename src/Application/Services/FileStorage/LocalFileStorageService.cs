using Domain.DTO;
using Microsoft.Extensions.Options;

namespace Application.Services.FileStorage
{
    public class LocalFileStorageService : ILocalFileStorageService
    {
        private readonly ApplicationSettings _settings;

        public LocalFileStorageService(IOptions<ApplicationSettings> settings)
        {
            _settings = settings.Value;
        }

        private string GetStoragePath()
        {
            return Directory.Exists(_settings.StoragePath) ? _settings.StoragePath : Path.GetTempPath();
        }

        public async Task<string> SaveFile(Stream fileStream, string filePath)
        {
            string fullPath = Path.Combine(GetStoragePath(), filePath);

            using (Stream stream = new FileStream(fullPath, FileMode.Create))
            {
                await fileStream.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}