using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.Utils.DDD;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure
{
    public static class RegistrationModule
    {
        public static void Register(Container container)
        {
            container.Register<IAggregateFactory, AggregateFactory>();

            RegisterCommandHandlers.AutoRegisterCommandHandlers(container);
            RepositoryRegistrations.Register(container);
        }
    }
}
