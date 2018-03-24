using Grayson.ExampleCQRS.Application.ReadModel.Services;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.ReadModel.Repository;
using Grayson.ExampleCQRS.KmStanden.Infrastructure.Registrations;
using Grayson.SeedWork.DDD.Domain;

using MassTransit;

using Microsoft.Extensions.Logging;

using SimpleInjector;

using System;

namespace Grayson.ExampleCQRS.Readmodel.Host.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                ILoggerFactory loggerFactory = new LoggerFactory()
                    .AddConsole()
                    .AddDebug();
                ILogger logger = loggerFactory.CreateLogger<Program>();
                container.RegisterSingleton<ILogger>(logger);
                logger.LogInformation("Starting BC 'ReadModel' host...");

                container.Options.AllowResolvingFuncFactories();

                ReadModel.Infrastructure.Registrations.InfrastructureModule.RegisterByConvention(
                    container,
                    new[] { typeof(KmStandViewRepository).Assembly });

                RabbitMqModule.RegisterEventConsumers(container);
                ReadModel.Infrastructure.Registrations.InfrastructureModule.RegisterAll(container);

                var typesToRegister = container.GetTypesToRegister(
                                            typeof(IDomainEventHandler<>),
                                            new[] { typeof(EventsProcessorService).Assembly },
                                            new TypesToRegisterOptions
                                            {
                                                IncludeGenericTypeDefinitions = true,
                                                IncludeComposites = false,
                                            });

                container.RegisterCollection(typeof(IDomainEventHandler<>), typesToRegister);

                container.RegisterSingleton(RabbitMqConfiguration.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.GetEventsQueue("ReadModel"), e =>
                    {
                        e.Handler<IDomainEvent>(context =>
                            Console.Out.WriteLineAsync($"Event received : {context.Message.GetType()}"));
                        e.LoadFrom(container);
                    });
                }));

                var bus = container.GetInstance<IBusControl>();

                bus.StartAsync();

                Console.WriteLine("Listening for events.. Press enter to exit");
                Console.ReadLine();

                bus.StopAsync();
            }
        }
    }
}