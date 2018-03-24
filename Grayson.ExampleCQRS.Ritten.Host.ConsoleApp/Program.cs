using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Ritten.Infrastructure.Registrations;
using Grayson.SeedWork.DDD.Domain;

using MassTransit;
using Microsoft.Extensions.Logging;
using SimpleInjector;

using System;

namespace Grayson.ExampleCQRS.Ritten.Host.ConsoleApp
{
    public class Program
    {
        private const string BoundedContextName = "Ritten";

        private static void Main()
        {
            using (var container = new Container())
            {
                ILoggerFactory loggerFactory = new LoggerFactory()
                    .AddConsole()
                    .AddDebug();
                ILogger logger = loggerFactory.CreateLogger<Program>();
                container.RegisterSingleton<ILogger>(logger);
                logger.LogInformation("Starting BC 'Ritten' host...");

                container.Options.AllowResolvingFuncFactories();

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
                    cfg.ReceiveEndpoint(host,
                        RabbitMqConstants.CommandsQueue, e =>
                        {
                            e.Handler<IDomainEvent>(context =>
                            Console.Out.WriteLineAsync($"Command received : {context.Message.GetType()}"));
                            e.LoadFrom(container);
                        });
                    // events queue
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.GetEventsQueue(BoundedContextName), e =>
                    {
                        e.Handler<IDomainEvent>(context =>
                            Console.Out.WriteLineAsync($"Event received : {context.Message.GetType()}"));
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