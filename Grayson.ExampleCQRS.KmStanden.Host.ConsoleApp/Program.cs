using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.KmStanden.Infrastructure.Integration;
using Grayson.ExampleCQRS.KmStanden.Infrastructure.Registrations;

using MassTransit;
using Microsoft.Extensions.Logging;
using SimpleInjector;

using System;

namespace Grayson.ExampleCQRS.KmStanden.Host.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            using (var container = new Container())
            {
                ILoggerFactory loggerFactory = new LoggerFactory()
                    .AddConsole()
                    .AddDebug();
                ILogger logger = loggerFactory.CreateLogger<Program>();
                container.RegisterSingleton<ILogger>(logger);
                logger.LogInformation("Starting BC 'KmStanden' host...");

                EventsMapping.Configure();

                container.Options.AllowResolvingFuncFactories();

                DomainModule.RegisterAll(container);
                ApplicationModule.RegisterAll(container);
                InfrastructureModule.RegisterAll(container);
                InfrastructureModule.RegisterEventForwarder(container);
                RabbitMqModule.RegisterCommandConsumers(container);

                ReadModel.Infrastructure.Registrations.InfrastructureModule.RegisterAll(container);

                container.RegisterSingleton(RabbitMqConfiguration.ConfigureBus((cfg, host) =>
                {
                    // command queue
                    cfg.ReceiveEndpoint(host,
                        RabbitMqConstants.CommandsQueue, e =>
                        {
                            e.LoadFrom(container);
                        });
                }));

                var bus = container.GetInstance<IBusControl>();

                bus.StartAsync();

                Console.WriteLine("Listening for commands.. Press enter to exit");
                Console.ReadLine();

                bus.StopAsync();
            }
        }
    }
}