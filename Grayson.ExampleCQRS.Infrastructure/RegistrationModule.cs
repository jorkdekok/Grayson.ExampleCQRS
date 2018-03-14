using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD.Domain;
using Grayson.Utils.DDD.Infrastructure;
using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure
{
    public static class RegistrationModule
    {
        public static void Register(Container container)
        {
            container.Register<IAggregateFactory, AggregateFactory>();

            ObjectFactory objectFactory = new ObjectFactory(container);
            container.RegisterSingleton<IObjectFactory>(objectFactory);

            //var eventPublisher = new EventPublisher(objectFactory);
            container.RegisterSingleton<IEventPublisher, EventPublisher>();


            MessageBusRegistrations.Register(container);
        }
    }
}