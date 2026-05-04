using Application.Common.Interfaces;
using Application.Issues.Queries.GetIssueDetailsQuery;
using Application.Services.ComicService;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;


namespace WebApp.Tools
{
    internal class ComicTools
    {
        private readonly IComicService _comicService;
        private readonly IGcdDbContext _context;
        private readonly IMediator _mediator;

        public ComicTools(IComicService comicService, IGcdDbContext context, IMediator mediator)
        {
            _comicService = comicService;
            _mediator = mediator;
            _context = context;
        }

        [McpServerTool]
        [Description("Returns the cover image of a comic")]
        public ImageContentBlock GetComicCoverImage([Description("The path to the comic archive")] string comicsPath)
        {
            string imagePath = _comicService.GetComicFirstPage(comicsPath);
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string mimeType = GetMimeType(imagePath);
            
            // Clean up: The directory for the extracted image is in the same directory as the image.
            string? directory = Path.GetDirectoryName(imagePath);
            if (directory != null && Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
            
            return ImageContentBlock.FromBytes(imageBytes, mimeType);
        }

        private string GetMimeType(string filePath)
        {
            byte[] buffer = new byte[8];
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer, 0, 8);
            }

            if (buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4E && buffer[3] == 0x47) return "image/png";
            if (buffer[0] == 0xFF && buffer[1] == 0xD8 && buffer[2] == 0xFF) return "image/jpeg";
            if (buffer[0] == 0x47 && buffer[1] == 0x49 && buffer[2] == 0x46) return "image/gif";
            
            return "application/octet-stream";
        }


        [McpServerTool]
        [Description("Writes the comic metadata to the archive")]
        public async Task<string> WriteComicMetadataToArchive([Description("The ID of the comic issue")] int issueId, [Description("The path to the comic archive")] string comicsPath)
        {
            CancellationToken stoppingToken = new CancellationToken();
            GetIssueDetailsQuery detailsQuery = new GetIssueDetailsQuery(issueId);
            GcdIssue issue = await _mediator.Send(detailsQuery, stoppingToken);
            ComicInfo comicInfo = _comicService.CreateComicInfo(issue);

            return _comicService.SaveComicInfo(comicInfo, comicsPath);
        }
    }
}
