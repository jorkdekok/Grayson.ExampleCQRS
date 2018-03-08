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

            container.Register(typeof(ICommandHandler<>), assemblies);

            container.Register<IServiceBus, AdvancedBus>();
        }
    }
}