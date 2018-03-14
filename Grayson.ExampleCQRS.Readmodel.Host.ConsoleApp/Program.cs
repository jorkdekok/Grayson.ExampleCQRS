using System;

using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Infrastructure;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.ReadModel.Application.Services;
using Grayson.Utils.DDD.Domain;

using MassTransit;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Readmodel.Host.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();

                var s = typeof(EventsProcessorService);

                RegistrationModule.Register(container);
                MessageBusRegistrations.RegisterEventConsumers(container);
                ReadModel.Infrastructure.Repository.RepositoryRegistrations.Register(container);

                container.RegisterSingleton(AdvancedBus.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.EventsQueue, e =>
                    {
                        e.Handler<IDomainEvent>(context =>
                            Console.Out.WriteLineAsync($"Event received : {context.Message.GetType()}"));
                        e.LoadFrom(container);
                    });
                }));

                container.Register<KmStand>();

                var bus2 = container.GetInstance<IBusControl>();

                bus2.StartAsync();

                Console.WriteLine("Listening for events.. Press enter to exit");
                Console.ReadLine();

                bus2.StopAsync();
            }
        }
    }
}