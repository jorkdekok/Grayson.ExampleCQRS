using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Grayson.ExampleCQRS.Infrastructure.Test
{
    [TestClass]
    public class MassTransitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var bus = new AdvancedBus(AdvancedBus.ConfigureBus());

            bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));

            var bus2 = AdvancedBus.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host,
                    RabbitMqConstants.CommandsQueue, e =>
                    {
                        //e.Consumer<MassTransitConsumer<AddNewKmStand>>();
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
