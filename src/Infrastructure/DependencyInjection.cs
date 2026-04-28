using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

#pragma warning disable CS8603 // Possible null reference return.
            services.AddScoped<IDbContext>(provider => provider.GetService<AppDbContext>());
#pragma warning restore CS8603 // Possible null reference return.

            return services;
        }
    }
}
