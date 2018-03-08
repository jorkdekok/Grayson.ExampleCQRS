using System;
using System.Threading.Tasks;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD;

using MassTransit;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Domain.Host.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();

                RegistrationModule.Register(container);

                // create a generic consumer for each command and register
                var assemblies = new[] { typeof(AddNewKmStand).Assembly };
                var commands = container.GetTypesToRegister(typeof(ICommand), assemblies);

                Type mtc = typeof(MassTransitCommandConsumer<>);

                foreach (var commandType in commands)
                {
                    Type real = mtc.MakeGenericType(commandType);
                    container.Register(real);
                }

                // events
                assemblies = new[] { typeof(KmStand).Assembly };
                var events = container.GetTypesToRegister(typeof(IDomainEvent), assemblies);

                Type mte = typeof(MassTransitEventConsumer<>);

                foreach (var eventType in events)
                {
                    Type real = mtc.MakeGenericType(eventType);
                    container.Register(real);
                }

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

                var bus2 = container.GetInstance<IBusControl>();

                bus2.StartAsync();

                Task.Factory.StartNew(() =>
                {
                    var busevents = AdvancedBus.ConfigureBus((cfg, host) =>
                    {
                        // events queue
                        cfg.ReceiveEndpoint(host,
                            RabbitMqConstants.EventsQueue, e =>
                            {
                                e.Consumer<TestConsumer>();
                            });
                    });

                    busevents.StartAsync();
                    Console.WriteLine("Listening for events...");
                    Console.ReadLine();
                    busevents.StopAsync();
                });

                Console.WriteLine("Listening for commands.. Press enter to exit");
                Console.ReadLine();

                bus2.StopAsync();
            }
        }

        public class TestConsumer : IConsumer<KmStandCreated>
        {
            public async Task Consume(ConsumeContext<KmStandCreated> context)
            {
                await Console.Out.WriteLineAsync($"Event: {context.Message.GetType()}");

                // update the customer address
            }
        }
    }
}