using Domain.DTO;
using Domain.Entities;
using Google.GenAI;
using Google.GenAI.Types;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Application.Services.Gemini
{
    public class GeminiClient : IGeminiClient
    {
        private readonly ApplicationSettings _settings;
        private readonly Client _client;

        public GeminiClient(IOptions<ApplicationSettings> settings)
        {
            _settings = settings.Value;
            _client = new Client(apiKey: _settings.Gemini.ApiKey);
        }

        public async Task<string> AnalyseImageAsync(string imageFilePath)
        {

            if (string.IsNullOrWhiteSpace(imageFilePath))
            {
                throw new ArgumentException("Image file path is required.", nameof(imageFilePath));
            }

            if (!System.IO.File.Exists(imageFilePath))
            {
                throw new FileNotFoundException("Image file not found.", imageFilePath);
            }

            Google.GenAI.Types.File uploadedFile = await _client.Files.UploadAsync(filePath: imageFilePath);

            string model = "gemma-3-4b-it";

            string prompt = "Based on this comic book cover, " +
                "extract a search term that I can use to look" +
                " for this specific issue in a comic book database. " +
                "Look for thinks like title, the series, the issue number or the publication year (in the yyyy format)." +
                " Your response will only include the search term with less than 4 words.";

            Content content = new Content
            {
                Role = "user",
                Parts = new List<Part>
                {
                    new Part { Text = prompt },
                    new Part
                    {
                        FileData = new FileData
                        {
                            FileUri = uploadedFile.Uri,
                            MimeType = uploadedFile.MimeType
                        }
                    }
                }
            };

            List<Content> contents = new List<Content> { content };

            GenerateContentResponse response = await _client.Models.GenerateContentAsync(model: model, contents: contents);

            string testResponse = string.Empty;

            if (!string.IsNullOrWhiteSpace(response.Text))
            {
                testResponse = response.Text;
            }
            else if (response.Candidates is not null &&
                     response.Candidates.Count > 0 &&
                     response.Candidates[0].Content is not null &&
                     response.Candidates[0]?.Content?.Parts is not null &&
                     response.Candidates[0]?.Content?.Parts?.Count > 0 &&
                     !string.IsNullOrWhiteSpace(response.Candidates[0]?.Content?.Parts?[0]?.Text))
            {
                testResponse = response.Candidates[0]?.Content?.Parts?[0]?.Text ?? string.Empty;
            }

            if (string.IsNullOrWhiteSpace(testResponse))
            {
                return string.Empty;
            }

            return testResponse;
        }
    }
}
