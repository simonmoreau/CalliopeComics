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

namespace Application.Test
{
    [Collection("DBContext")]
    public class SettingsServerTest : BaseTestClass
    {
        public SettingsServerTest(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }

        [Fact]
        public async Task GetSettingsTest()
        {
            // Arrange
            ApplicationSettings appSettings = new ApplicationSettings
            {
                StoragePath = "C:\\CalliopeComicsServer\\Storage",
            };

            IOptions<ApplicationSettings> options = Options.Create(appSettings);
            GetSettingsQueryHandler request = new GetSettingsQueryHandler(options);

            // Act  
            ApplicationSettings result = await request.Handle(new GetSettingsQuery(), CancellationToken.None);

            // Assert  
            Assert.NotNull(result);
            Assert.IsType<ApplicationSettings>(result);
            Assert.Equal("C:\\CalliopeComicsServer\\Storage", result.StoragePath);
        }
    }
}