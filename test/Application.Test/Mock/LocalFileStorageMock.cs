using Application.Services.FileStorage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Test.Mock
{
    internal class LocalFileStorageMock : ILocalFileStorageService
    {
        private string _storagePath;
        public LocalFileStorageMock()
        {
            string baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            if (!Directory.Exists(baseFolder))
            {
                baseFolder = Path.GetTempPath();
            }

            _storagePath = Path.Combine(baseFolder, "CalliopeComics");

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public string GetStoragePath()
        {
            return _storagePath;
        }

        public async Task<string> SaveFile(Stream fileStream, string filePath)
        {
            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
