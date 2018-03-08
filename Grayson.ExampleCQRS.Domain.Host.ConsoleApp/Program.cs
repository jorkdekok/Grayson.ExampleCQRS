using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using MassTransit;
using System;

namespace Grayson.ExampleCQRS.Domain.Host.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus2 = AdvancedBus.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host,
                    RabbitMqConstants.CommandsQueue, e =>
                    {
                        e.Consumer<MassTransitConsumer<AddNewKmStand>>();
                    });
            });

            bus2.StartAsync();

            Console.WriteLine("Listening for Register order commands.. " +
                              "Press enter to exit");
            Console.ReadLine();

            bus2.StopAsync();
        }
    }
}
