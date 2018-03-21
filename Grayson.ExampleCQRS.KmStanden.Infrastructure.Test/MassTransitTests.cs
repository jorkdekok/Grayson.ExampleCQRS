using System;
using System.Threading.Tasks;

using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.KmStanden.Application.Commands;
using Grayson.SeedWork.DDD.Application;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grayson.ExampleCQRS.KmStanden.Infrastructure.Test
{
    [TestClass]
    public class MassTransitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            ICommandBus bus = new AdvancedBus(RabbitMqConfiguration.ConfigureBus());

            Task.Factory.StartNew(() => bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty)));

            var bus2 = RabbitMqConfiguration.ConfigureBus((cfg, host) =>
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