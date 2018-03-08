using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Rebus.Config;
using Rebus.SimpleInjector;
using Rebus.Transport.InMem;
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
                //container.RegisterCollection<IHandleMessages<string>>(new[] { typeof(StringHandler) });
                RegisterCommandHandlers.AutoRegisterCommandHandlers(container);

                var bus = Configure.With(new SimpleInjectorContainerAdapter(container))
                    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "simple-injector-test"))
                    .Start();

                

                bus.Advanced.SyncBus.SendLocal(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }
    }
}
