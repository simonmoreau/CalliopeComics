using Microsoft.AspNetCore.Authentication.Negotiate;
using Serilog;
using Serilog.Events;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;
using System.Reflection;

namespace WebApp
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration, bool isAuthenticationEnabled)
        {
            if (isAuthenticationEnabled)
            {
                services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                    .AddNegotiate();
            }

            return services;
        }

        public static IServiceCollection AddSerilogServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Serilog
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration().ReadFrom.Configuration(configuration);

            string outputTemplate = "{Timestamp:HH:mm:ss} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}";
            loggerConfiguration
                .MinimumLevel.Override("CalliopeComicsServer.Application.Authentication.GetRolesQueryHandler", LogEventLevel.Information);

            loggerConfiguration
                .MinimumLevel.Override("CalliopeComicsServer.Controllers", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .MinimumLevel.Override("CalliopeComicsServer.Application.Common.Behaviors.LoggingBehavior", LogEventLevel.Warning);

            loggerConfiguration
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: outputTemplate);

            


            // Check if the database is available
            try
            {
                loggerConfiguration.WriteTo.PostgreSQL(
                    connectionString: configuration.GetConnectionString("LogDatabase"),

                    tableName: "logs",
                    needAutoCreateTable: true);
            }
            catch (Exception ex)
            {

            }

            Log.Logger = loggerConfiguration.CreateLogger();
            Log.Information("Starting up");

            Version? version = Assembly.GetExecutingAssembly().GetName().Version;
            if (version != null)
            {
                Log.Information($"Current version : {version.ToString()}");
            }

            services.AddSerilog();

            return services;

        }
    }
}