
namespace Application.Services.FileStorage
{
    public interface ILocalFileStorageService
    {
        public Task<string> SaveFile(Stream fileStream, string fileName);
    }
}