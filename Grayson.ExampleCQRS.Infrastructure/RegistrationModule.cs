using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD.Domain;

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