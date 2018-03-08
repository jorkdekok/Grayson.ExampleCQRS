using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.Utils.DDD;
using MassTransit;
using SimpleInjector;
using System;

namespace Grayson.ExampleCQRS.Domain.Host.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();

                RegisterCommandHandlers.AutoRegisterCommandHandlers(container);
                RepositoryRegistrations.Register(container);

                // create a generic consumer for each command and register 
                var assemblies = new[] { typeof(AddNewKmStand).Assembly };
                var commands = container.GetTypesToRegister(typeof(ICommand), assemblies);

                Type mtc= typeof(MassTransitConsumer<>);
                
                foreach (var commandType in commands)
                {
                    Type real = mtc.MakeGenericType(commandType);
                    container.Register(real);
                }

                container.RegisterSingleton(AdvancedBus.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host,
                        RabbitMqConstants.CommandsQueue, e =>
                        {
                            e.LoadFrom(container); // .Consumer<MassTransitConsumer<AddNewKmStand>>();
                            //e.Consumer<MassTransitConsumer<AddNewKmStand>>(container);
                            //e.Consumer(container);
                            
                        });
                }));

                var r = container.GetInstance<IRepository<KmStand>>();

                var bus2 = container.GetInstance<IBusControl>();

                bus2.StartAsync();

                Console.WriteLine("Listening for commands.. Press enter to exit");
                Console.ReadLine();

                bus2.StopAsync();
            }
        }
    }
}