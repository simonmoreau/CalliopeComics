using Application.Test.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test
{
    [Collection("DBContext")]
    public class DbContextTest : BaseTestClass
    {
        public DbContextTest(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }

        // V�rifier que les donn�es sont bien charg�s depuis la base

        [Fact]
        public void TestDataRetrieval()
        {
            // Assert
            Assert.NotEmpty(_context.CLIENTS);
        }

        // V�rifier qu'il y a bien le bon nombre de donn�es

        [Fact]
        public void TestElementCounts()
        {
            // Assert
            Assert.Equal(2, _context.CLIENTS.Count());

        }

        // V�rifier que les mod�les qui comporte des listes chargent bien

        [Fact]
        public void TestInitializeList()
        {
            // Arrange
            _context.CLIENTS.ToList();

            // Act
            var client = _context.CLIENTS.FirstOrDefault(a => a.CLIENTID == 1);

            // Assert
            Assert.NotNull(client.Created);
        }
    }
}