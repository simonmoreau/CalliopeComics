using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using System;
using System.IO;
using System.Threading.Tasks;
using Infrastructure;
using Xunit;


namespace Application.Test.Common
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private static readonly object _lock = new();
        private static bool _databaseInitialized;
        private string _connectionString = string.Empty;

        internal ServiceProvider? ServiceProvider { get; private set; }

        public DatabaseFixture()
        {
            string dbName = "CalliopeComicstest_" + Guid.NewGuid().ToString("N");
            _connectionString = GetConnectionString(dbName);
        }

        public async Task InitializeAsync()
        {
            ServiceProvider = BuildService();
            await CreateDB(ServiceProvider);
        }

        public async Task DisposeAsync()
        {
            await Destroy();
        }

        private ServiceProvider BuildService()
        {
            IdentityBuilder identityBuilder = new ServiceCollection()
                .AddIdentityCore<ApplicationUser>().AddRoles<ApplicationRole>().AddEntityFrameworkStores<AppDbContext>();

            identityBuilder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_connectionString,
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    })
                );

            identityBuilder.Services.AddScoped<IDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            identityBuilder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            identityBuilder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<IDbContext>();
            });

            ServiceProvider serviceProvider = identityBuilder.Services.BuildServiceProvider();

            return serviceProvider;

        }

        private static string GetConnectionString(string dbName)
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(GetBaseConnectionString())
            {
                Database = dbName
            };
            string connectionString = builder.ToString();
            return connectionString;
        }

        private static string GetBaseConnectionString()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string appSettingsPath = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", "..", "..", "..", "src", "WebApp", "appsettings.Development.json"));
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(appSettingsPath) ?? baseDirectory)
                .AddJsonFile(Path.GetFileName(appSettingsPath), optional: false, reloadOnChange: false)
                .Build();

            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("DefaultConnection is missing from appsettings.Development.json.");
            }

            return connectionString;
        }

        private async Task CreateDB(ServiceProvider serviceProvider)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IServiceProvider scopedServices = scope.ServiceProvider;
                AppDbContext context = (AppDbContext)serviceProvider.GetRequiredService<IDbContext>();

                lock (_lock)
                {
                    if (!_databaseInitialized)
                    {
                        context.Database.EnsureCreated();

                        _databaseInitialized = true;
                    }
                }

                DbContextInitialiser dbContextInitialiser = new DbContextInitialiser(NullLogger<DbContextInitialiser>.Instance, context);

                await dbContextInitialiser.SeedAsync(serviceProvider);

            }
        }

        private async Task Destroy()
        {
            using (IServiceScope scope = ServiceProvider.CreateScope())
            {
                IServiceProvider scopedServices = scope.ServiceProvider;
                AppDbContext context = (AppDbContext)scopedServices.GetRequiredService<IDbContext>();

                await context.Database.EnsureDeletedAsync();

                context.Dispose();
            }

        }
    }
}
