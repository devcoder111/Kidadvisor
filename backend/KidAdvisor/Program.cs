using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KidAdvisor;

namespace KidAdvisor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetService<BusinessContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error initializing project (database migration or scheduler)");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
                           WebHost.CreateDefaultBuilder(args)
                                .ConfigureLogging(logging =>
                                {
                                    logging.ClearProviders();
                                    logging.AddConsole();
                                })
                               .UseStartup<Startup>()
                               .Build();

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            var baseConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(baseConfig)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var env = builderContext.HostingEnvironment;
                    config.SetBasePath(env.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .UseStartup<Startup>()
                .Build();
        }
    }
}
