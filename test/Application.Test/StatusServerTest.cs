using Application.Test.Common;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Domain.DTO;
using Application.Status.Queries.GetStatusQuery;

namespace Application.Test
{
    [Collection("DBContext")]
    public class StatusServerTest : BaseTestClass
    {
        public StatusServerTest(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }

        [Fact]
        public async Task GetStatusTest()
        {
            // Arrange
            ApplicationSettings appSettings = new ApplicationSettings
            {
                StoragePath = "C:\\CalliopeComicsServer\\Storage",
            };

            IOptions<ApplicationSettings> options = Options.Create(appSettings);
            GetStatusQueryHandler request = new GetStatusQueryHandler(_context, options);

            // Act  
            StatusDTO result = await request.Handle(new GetStatusQuery(), CancellationToken.None);

            // Assert  
            Assert.NotNull(result);
            Assert.IsType<StatusDTO>(result);
            Assert.True(result.DatabaseConnected);
            Assert.False(result.IsAuthenticated);
            Assert.Equal("1.1.0.0", result.Version);
        }
    }
}