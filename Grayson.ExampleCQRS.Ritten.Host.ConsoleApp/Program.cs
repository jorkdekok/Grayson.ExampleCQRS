using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Ritten.Application.IntegrationEvents;
using Grayson.ExampleCQRS.Ritten.Application.Services;
using Grayson.ExampleCQRS.Ritten.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Ritten.Infrastructure.Registrations;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.SeedWork.DDD.Domain;
using Grayson.Utils.Configuration;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

using SimpleInjector;

using System;
using System.IO;

namespace Grayson.ExampleCQRS.Ritten.Host.ConsoleApp
{
    public class Program
    {
        private const string BoundedContextName = "Ritten";

        private static void Main()
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();
                // configuration appsettings convention
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                container.Options.RegisterParameterConvention(new AppSettingsConvention(key => config[key]));

                ILoggerFactory loggerFactory = new LoggerFactory()
                    .AddConsole()
                    .AddDebug();
                ILogger logger = loggerFactory.CreateLogger<Program>();
                container.RegisterSingleton<ILogger>(logger);
                logger.LogInformation("Starting BC 'Ritten' host...");

                container.RegisterSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
                container.RegisterSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
                container.RegisterSingleton<IConnectionFactory, ConnectionFactory>();
                container.RegisterSingleton<IIntegrationEventBus, EventBusRabbitMQ>();

                DomainModule.RegisterAll(container);
                ApplicationModule.RegisterAll(container);
                InfrastructureModule.RegisterAll(container);
                //InfrastructureModule.RegisterEventForwarder(container);
                RabbitMqModule.RegisterCommandConsumers(container);
                RabbitMqModule.RegisterEventConsumers(container);

                //ReadModel.Infrastructure.Registrations.InfrastructureModule.RegisterAll(container);

                container.RegisterSingleton(RabbitMqConfiguration.ConfigureBus((cfg, host) =>
                {
                    // command queue
                    //cfg.ReceiveEndpoint(host,
                    //    RabbitMqConstants.CommandsQueue, e =>
                    //    {
                    //        e.Handler<ICommand>(context =>
                    //        Console.Out.WriteLineAsync($"Command received : {context.Message.GetType()}"));
                    //        //e.LoadFrom(container);// TODO: prevent receiving same events
                    //    });
                    // events queue
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.GetEventsQueue(BoundedContextName), e =>
                    {
                        e.Handler<IDomainEvent>(context =>
                            Console.Out.WriteLineAsync($"Event received : {context.Message.GetType()}"));
                        e.LoadFrom(container);
                    });
                }));

                EventMappings.Configure();

                var eventBus = container.GetInstance<IIntegrationEventBus>();
                eventBus.Subscribe<KmStandCreated, RitService>();

                //var bus = container.GetInstance<IBusControl>();

                //bus.StartAsync();

                Console.WriteLine("Listening for commands.. Press enter to exit");
                Console.ReadLine();

                //bus.StopAsync();
            }
        }
    }
}