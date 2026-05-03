using Application.Services.ComicService;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace WebApp.Tools
{
    internal class ComicTools
    {
        private readonly IComicService _comicService;

        public ComicTools(IComicService comicService)
        {
            _comicService = comicService;
        }

        [McpServerTool]
        [Description("Returns the cover image of a comic")]
        public ImageContentBlock GetComicCoverImage([Description("The path to the comic archive")] string comicsPath)
        {
            string imagePath = _comicService.GetComicFirstPage(comicsPath);
            byte[] pngBytes = File.ReadAllBytes(imagePath);
            
            // Clean up: The directory for the extracted image is in the same directory as the image.
            string? directory = Path.GetDirectoryName(imagePath);
            if (directory != null && Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
            
            return ImageContentBlock.FromBytes(pngBytes, "image/png");
        }
    }
}
