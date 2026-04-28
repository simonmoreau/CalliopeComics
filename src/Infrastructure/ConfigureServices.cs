using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Infrastructure.AppDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            builder =>
            {
                builder.MigrationsAssembly(typeof(Infrastructure.AppDbContext).Assembly.FullName);
            }));

        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<Infrastructure.AppDbContext>());
        services.AddScoped<Infrastructure.DbContextInitialiser>();

        return services;
    }
}
