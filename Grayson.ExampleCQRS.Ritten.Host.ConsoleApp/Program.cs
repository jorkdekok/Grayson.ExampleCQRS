using System;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Ritten.Infrastructure.Registrations;
using Grayson.SeedWork.DDD.Domain;
using MassTransit;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Ritten.Host.ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                Console.WriteLine("Starting BC 'Ritten' host...");

                container.Options.AllowResolvingFuncFactories();

                DomainModule.RegisterAll(container);
                ApplicationModule.RegisterAll(container);
                InfrastructureModule.RegisterAll(container);
                InfrastructureModule.RegisterEventForwarder(container);
                RabbitMqModule.RegisterCommandConsumers(container);
                RabbitMqModule.RegisterEventConsumers(container);

                ReadModel.Infrastructure.Registrations.InfrastructureModule.RegisterAll(container);

                //container.Register<IKmStandRepository, KmStandRepository>();
                //container.Register<IRitRepository, RitRepository>();

                container.RegisterSingleton(RabbitMqConfiguration.ConfigureBus((cfg, host) =>
                {
                    // command queue
                    cfg.ReceiveEndpoint(host,
                        RabbitMqConstants.CommandsQueue, e =>
                        {
                            e.LoadFrom(container);
                        });
                    // events queue
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.EventsQueue, e =>
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