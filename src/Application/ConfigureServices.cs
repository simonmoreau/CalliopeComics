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
                cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvY" +
                "mJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1" +
                "Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODA4ODcwNDA" +
                "wIiwiaWF0IjoiMTc3NzQwNDQzNSIsImFjY291bnRfaWQiOiIwMTlkZDU4ZWZiYTE3ZTk4YTFkMzgwYWEwZmZjNTR" +
                "jZSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa3FhcnlwdzR2bWd4ZjU2MmczdjF2OXI4Iiwic3ViX2lkIjoiLSIsImV" +
                "kaXRpb24iOiIwIiwidHlwZSI6IjIifQ.dXTYVa-slRPj4xBZpjuugXgDNKw7vhobuidY7KvKfm44qlq2w6WcbVP" +
                "BDtRlFZkxtefgsAdWqrEWKkSvuDGXhGk8HUAcRHRhgJl9VZYae1ZXkQ-BU-Sk1hQKB5YcrSpyAxP4_u7LqhoM8" +
                "qT7folfJFprAau_lGK379G0d3ZSGK6gVfTEoPLjZfUj6SQT_FwsQtjH-AicNNCVUPfeDZatr5RlomzJ1sfc8MkK" +
                "Y8JSBU0VqGjHOyJpZfSqNFYXGtDsM08E9i33kEIvWsRK9TgANTU5hdvJq2Kw3KWJrBSZ_OZVirpI19M3UrU0Jh" +
                "rCtnpJ035MOZX-82zNPgTHL-vLEQ";

                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddSingleton<ILocalFileStorageService, LocalFileStorageService>();
            services.AddSingleton<IGeminiClient, GeminiClient>();
            services.AddSingleton<IComicService, ComicService>();
            //services.AddHostedService<ComicsProcessorService>();
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