using System.Text.Json.Serialization;
using System.Text.Json;

namespace Infrastructure.Models
{
    internal class Helper
    {
        internal async static Task<T> LoadObjectFromJson<T>(string jsonFileName)
        {
            string jsonPath = $"{AppContext.BaseDirectory}\\Ressources\\{jsonFileName}.json";
            string jsonContent = File.ReadAllText(jsonPath);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            T data = JsonSerializer.Deserialize<T>(jsonContent, options);
            return await Task.FromResult(data);
        }

        internal static string CopyImageFromRessources(string storagePath, string fileName)
        {
            string imageFilePath = $"{AppContext.BaseDirectory}\\Ressources\\{fileName}";
            string destinationPath = Path.Combine(storagePath, fileName);
            string destinationDirectory = Path.GetDirectoryName(destinationPath);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            if (!File.Exists(destinationPath) && File.Exists(imageFilePath))
            {
                File.Copy(imageFilePath, destinationPath);
            }

            return destinationPath;
        }
    }
}
