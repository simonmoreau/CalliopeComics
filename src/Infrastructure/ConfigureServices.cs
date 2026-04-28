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

                services.AddDbContext<Infrastructure.GcdDbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("GcdConnection"),
            builder =>
            {
                builder.MigrationsAssembly(typeof(Infrastructure.GcdDbContext).Assembly.FullName);
            }));

        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<Infrastructure.AppDbContext>());
        services.AddScoped<IGcdDbContext>(provider => provider.GetRequiredService<Infrastructure.GcdDbContext>());
        services.AddScoped<Infrastructure.DbContextInitialiser>();

        return services;
    }
}
