using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Infrastructure
{
    public class DbContextInitialiser
    {
        private readonly ILogger<DbContextInitialiser> _logger;
        private readonly AppDbContext _context;

        public DbContextInitialiser(ILogger<DbContextInitialiser> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsNpgsql())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            try
            {
                await TrySeedAsync(serviceProvider);
                // await TrySeedProdAsync(serviceProvider);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync(IServiceProvider serviceProvider)
        {
            if (_context.CLIENTS.Any())
            {
                return;
            }

            ApplicationSettings appSettings = serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>().Value;

            //List<CLIENT> clients = new List<CLIENT>
            //{
            //    new CLIENT
            //    {
            //        NAME = "Client1",
            //        IP = "192.168.1.1"
            //    },
            //    new CLIENT
            //    {
            //        NAME = "Client2",
            //        IP = "192.168.1.2"
            //    }
            //};
            //_context.CLIENTS.AddRange(clients);


            await _context.SaveChangesAsync();
        }
    }
}