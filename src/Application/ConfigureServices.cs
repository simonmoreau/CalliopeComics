using Application.Common.Behaviors;
using Application.Services.FileStorage;
using Domain.DTO;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Interfaces
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddSingleton<ILocalFileStorageService, LocalFileStorageService>();




            return services;
        }

    }
}