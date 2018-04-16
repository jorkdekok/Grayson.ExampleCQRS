using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.ReadModel.Repository;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.Utils.Configuration;
using Grayson.Utils.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SimpleInjector;

using System;
using System.IO;

namespace Grayson.ExampleCQRS.Readmodel.Host.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();
                // configuration appsettings convention
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
                container.Options.RegisterParameterConvention(new AppSettingsConvention(key => config[key]));

                ILoggerFactory loggerFactory = new LoggerFactory()
                    .AddConsole()
                    .AddDebug();
                container.Options.DependencyInjectionBehavior = new MsContextualLoggerInjectionBehavior(loggerFactory, container);

                ILogger logger = loggerFactory.CreateLogger<Program>();
                //container.RegisterSingleton<ILogger>(logger);
                logger.LogInformation("Starting BC 'ReadModel' host...");

                ReadModel.Infrastructure.Registrations.InfrastructureModule.RegisterByConvention(
                    container,
                    new[] { typeof(KmStandViewRepository).Assembly });

                ReadModel.Infrastructure.Registrations.InfrastructureModule.RegisterAll(container);

                ExampleCQRS.Infrastructure.Registrations.InfrastructureModule.RegisterServices(container);
                ExampleCQRS.Infrastructure.Registrations.InfrastructureModule.RegisterEventBus(container, config);

                using (var eventBus = container.GetInstance<IIntegrationEventBus>())
                {

                    Console.WriteLine("Listening for events.. Press enter to exit");
                    Console.ReadLine();
                }
            }
        }
    }
}