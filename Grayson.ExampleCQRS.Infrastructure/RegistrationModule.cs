using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.Utils.DDD;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure
{
    public static class RegistrationModule
    {
        public static void Register(Container container)
        {
            container.Register<IAggregateFactory, AggregateFactory>();

            MessageBusRegistrations.Register(container);
            
        }
    }
}