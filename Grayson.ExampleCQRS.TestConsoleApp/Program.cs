using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD;
using MassTransit;
using SimpleInjector;
using System;

namespace Grayson.ExampleCQRS.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();

                RegisterCommandHandlers.AutoRegisterCommandHandlers(container);

                container.Register<IServiceBus, AdvancedBus>();
                container.RegisterSingleton(AdvancedBus.ConfigureBus());

                var bus = container.GetInstance<IServiceBus>();

                bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));
                                
                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }
    }
}
