using Grayson.Utils.DDD.Domain;
using Grayson.Utils.DDD.Infrastructure;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.Registrations
{
    public static class DomainModule
    {
        public static void RegisterAll(Container container)
        {
            // Factories
            container.Register<IAggregateFactory, AggregateFactory>();

            // event publisher
            container.RegisterSingleton<IEventPublisher, EventPublisher>();
        }
    }
}