using Application.Interfaces;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.Hosting.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CLI
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    string serverDirectoryName = AppDomain.CurrentDomain.FriendlyName;
                    string serverDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), serverDirectoryName);

                    if (!Directory.Exists(serverDirectory))
                    {
                        Directory.CreateDirectory(serverDirectory);
                    }

                    string serverSettingsPath = Path.Combine(serverDirectory, "appsettings.Server.json");

                    if (!File.Exists(serverSettingsPath))
                    {
                        File.WriteAllText(serverSettingsPath, "{}");
                    }

                    configurationBuilder
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile(serverSettingsPath, optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddApplicationServices(context.Configuration);
                    services.AddInfrastructureServices(context.Configuration);
                });

            try
            {
                return await builder.RunCommandLineApplicationAsync<ComicCmd>(args);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return -1;
            }
        }
    }
}
