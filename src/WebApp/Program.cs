using Application.Common.Behaviors;
using Application.Interfaces;
using Domain.DTO;
using Infrastructure;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApp.Tools;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string _serverDirectoryName = AppDomain.CurrentDomain.FriendlyName;
            string _serverDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _serverDirectoryName);

            if (!Directory.Exists(_serverDirectory))
            {
                Directory.CreateDirectory(_serverDirectory);
            }

            string _serverSettingsPath = Path.Combine(_serverDirectory, "appsettings.Server.json");

            if (!File.Exists(_serverSettingsPath))
            {
                File.WriteAllText(_serverSettingsPath, "{}");
            }

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(_serverSettingsPath, optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Add services to the container.
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddSerilogServices(builder.Configuration);

            ApplicationSettings? appSettings = builder.Configuration
                .GetSection(nameof(ApplicationSettings))
                .Get<ApplicationSettings>();

            bool isAuthenticationEnabled = appSettings?.Authentication?.Enabled ?? false;

            builder.Services.AddIdentityServices(builder.Configuration, isAuthenticationEnabled);

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(RequestGenericExceptionHandler<,,>));

            builder.Services.AddWindowsService();
            builder.Services.AddProblemDetails();

            // Add the MCP services: the transport to use (http) and the tools to register.
            builder.Services
                .AddMcpServer()
                .WithHttpTransport(options =>
                {
                    // Stateless mode is recommended for servers that don't need
                    // server-to-client requests like sampling or elicitation.
                    // See https://csharp.sdk.modelcontextprotocol.io/concepts/transports/transports.html for details.
                    options.Stateless = true;
                })
                .WithTools<GcdTools>()
                .WithTools<ComicTools>();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
                options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);
            });

            WebApplication app = builder.Build();

            bool shouldMigrate = Array.Exists(args, argument => string.Equals(argument, "--migrate", StringComparison.OrdinalIgnoreCase));

            if (shouldMigrate)
            {
                using IServiceScope scope = app.Services.CreateScope();
                DbContextInitialiser ApPInitialiser = scope.ServiceProvider.GetRequiredService<DbContextInitialiser>();
                await ApPInitialiser.InitialiseAsync();
                return;
            }

            if (app.Environment.IsDevelopment())
            {
                using (IServiceScope scope = app.Services.CreateScope())
                {
                    DbContextInitialiser ApPInitialiser = scope.ServiceProvider.GetRequiredService<DbContextInitialiser>();
                    await ApPInitialiser.InitialiseAsync();
                    await ApPInitialiser.SeedAsync(scope.ServiceProvider);
                }
            }

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "swagger";
            });

            string defaultStoragePath = Path.Combine(app.Environment.ContentRootPath, "data", "CalliopeComics");
            string storagePath = string.IsNullOrWhiteSpace(appSettings?.StoragePath)
                ? defaultStoragePath
                : appSettings.StoragePath;

            storagePath = Path.IsPathRooted(storagePath)
                ? Path.GetFullPath(storagePath)
                : Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, storagePath));

            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(storagePath),
                RequestPath = "/files",
            });

            app.MapMcp();
            app.UseHttpsRedirection();


            if (isAuthenticationEnabled)
            {
                app.UseAuthentication();
            }

            app.UseAuthorization();

            ControllerActionEndpointConventionBuilder controllers = app.MapControllers();

            if (isAuthenticationEnabled)
            {
                controllers.RequireAuthorization();
            }

            app.UseCors("Open");

            app.MapFallbackToFile("index.html");

            app.Run();
        }

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Infrastructure.AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Database=pre-met_ory4;Username=postgres;Password=postgres;"));

            services.AddCors(options =>
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}