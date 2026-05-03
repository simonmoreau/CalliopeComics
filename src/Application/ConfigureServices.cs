using Application.Common.Behaviors;
using Application.Services;
using Application.Services.ComicService;
using Application.Services.FileStorage;
using Application.Services.Gemini;
using Application.Services.GrandComicDatabase;
using Domain.DTO;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
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
            services.AddSingleton<IGeminiClient, GeminiClient>();
            services.AddSingleton<IComicService, ComicService>();
            services.AddHostedService<ComicsProcessorService>();
            services.AddSingleton<RequestSender>();

            services.AddHttpClient<IGrandComicDatabaseClient, GrandComicDatabaseClient>(client =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri("https://www.comics.org/api/");
            })
             .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
             {
                 AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
             });

            return services;
        }

    }
}