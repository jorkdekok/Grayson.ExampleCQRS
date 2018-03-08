using Grayson.Utils.DDD;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public static class RegisterCommandHandlers
    {
        public static void AutoRegisterCommandHandlers(Container container)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Console.WriteLine(assembly.GetName());
            }
            container.Register(typeof(ICommandHandler<>), assemblies);

            container.Register<IServiceBus, AdvancedBus>();
        }
    }
}