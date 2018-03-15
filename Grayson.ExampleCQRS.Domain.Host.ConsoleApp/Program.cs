using System;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.Utils.DDD.Application;

using MassTransit;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Domain.Host.ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();

                RegistrationModule.Register(container);
                MessageBusRegistrations.RegisterCommandConsumers(container);
                RepositoryRegistrations.Register(container);

                container.RegisterSingleton(AdvancedBus.ConfigureBus((cfg, host) =>
                {
                    // command queue
                    cfg.ReceiveEndpoint(host,
                        RabbitMqConstants.CommandsQueue, e =>
                        {
                            e.LoadFrom(container);
                        });
                }));

                container.Register<KmStand>();
                var r = container.GetInstance<IRepository<KmStand>>();
                container.Verify();
                var c = container.GetInstance<ICommandHandler<AddNewKmStand>>();

                var bus2 = container.GetInstance<IBusControl>();

                bus2.StartAsync();

                Console.WriteLine("Listening for commands.. Press enter to exit");
                Console.ReadLine();

                bus2.StopAsync();
            }
        }
    }
}