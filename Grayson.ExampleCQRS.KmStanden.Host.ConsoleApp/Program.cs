using System;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.KmStanden.Infrastructure.Registrations;
using MassTransit;

using SimpleInjector;

namespace Grayson.ExampleCQRS.KmStanden.Host.ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                Console.WriteLine("Starting BC 'KmStanden' host...");

                container.Options.AllowResolvingFuncFactories();

                DomainModule.RegisterAll(container);
                ApplicationModule.RegisterAll(container);
                InfrastructureModule.RegisterAll(container);
                InfrastructureModule.RegisterEventForwarder(container);
                RabbitMqModule.RegisterCommandConsumers(container);

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