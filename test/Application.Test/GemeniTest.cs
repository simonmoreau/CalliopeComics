using Application.Test.Common;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Domain.DTO;
using Application.Settings.Queries;
using Application.Services.Gemini;

namespace Application.Test
{
    [Collection("DBContext")]
    public class GemeniTest : BaseTestClass
    {
        public GemeniTest(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }

        [Fact]
        public async Task GemeniImageTest()
        {
            // Arrange
            ApplicationSettings appSettings = new ApplicationSettings
            {
                StoragePath = "C:\\CalliopeComicsServer\\Storage"
            };

            IOptions<ApplicationSettings> options = Options.Create(appSettings);

            GeminiClient geminiClient = new GeminiClient(options);

            string prompt = "Based on this comic book cover, extract a search term that I can use to look for this specific issue in a comic book database. I want a single string with a few keywords.";
            string imageFilePath = @"C:\Users\smoreau\Downloads\Avengers_ANN_1998.png";

            string result = await geminiClient.AnalyseImageAsync(prompt, imageFilePath);
            // Assert  
            Assert.NotNull(result);
            Assert.IsType<ApplicationSettings>(result);
        }
    }
}