using System;

using Grayson.ExampleCQRS.Application.ReadModel.Services;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Registrations;
using Grayson.Utils.DDD.Domain;

using MassTransit;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Readmodel.Host.ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                Console.WriteLine("Starting readmodel host...");
                container.Options.AllowResolvingFuncFactories();

                RabbitMqModule.RegisterEventConsumers(container);
                Infrastructure.ReadModel.Registrations.InfrastructureModule.RegisterAll(container);

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
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.EventsQueue, e =>
                    {
                        e.Handler<IDomainEvent>(context =>
                            Console.Out.WriteLineAsync($"Event received : {context.Message.GetType()}"));
                        e.LoadFrom(container);
                    });
                }));

                //container.Register<KmStand>();

                var bus = container.GetInstance<IBusControl>();

                bus.StartAsync();

                Console.WriteLine("Listening for events.. Press enter to exit");
                Console.ReadLine();

                bus.StopAsync();
            }
        }
    }
}