using System;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD;

using SimpleInjector;

namespace Grayson.ExampleCQRS.TestConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();

                MessageBusRegistrations.Register(container);

                container.RegisterSingleton(AdvancedBus.ConfigureBus());

                var bus = container.GetInstance<IMessgeBus>();

                bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }
    }
}