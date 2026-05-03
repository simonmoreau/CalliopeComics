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
using System.Net.Http;
using Application.Services.GrandComicDatabase;

namespace Application.Test
{
    [Collection("DBContext")]
    public class GemeniTest : BaseTestClass
    {
        public GemeniTest(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }

        [Fact(Skip = "Requires external service")]
        public async Task GemeniImageTest()
        {
            // Arrange
            ApplicationSettings appSettings = new ApplicationSettings
            {
                StoragePath = "C:\\CalliopeComicsServer\\Storage"
            };

            IOptions<ApplicationSettings> options = Options.Create(appSettings);

            GeminiClient geminiClient = new GeminiClient(options);

            string imageFilePath = @"C:\Users\smoreau\Downloads\Avengers_ANN_1998.png";

            string result = await geminiClient.AnalyseImageAsync(imageFilePath, imageFilePath);
            // Assert  
            Assert.NotNull(result);
        }

        [Fact(Skip = "Requires external service")]
        public async Task GcdImageTest()
        {
            // Arrange
            ApplicationSettings appSettings = new ApplicationSettings
            {
                StoragePath = "C:\\CalliopeComicsServer\\Storage"
            };

            IOptions<ApplicationSettings> options = Options.Create(appSettings);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.comics.org/api/");

            GrandComicDatabaseClient gcdClient = new GrandComicDatabaseClient(httpClient, new Services.RequestSender(options));

            string imageFilePath = @"C:\Users\smoreau\Downloads\Avengers_ANN_1998.png";

            string result = await gcdClient.AnalyseImageAsync(imageFilePath, imageFilePath);
            // Assert  
            Assert.NotNull(result);
        }
    }
}