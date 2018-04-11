using System;

using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.SeedWork.DDD.Domain;
using RabbitMQ.Client;
using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.Registrations
{
    public static class InfrastructureModule
    {
        public static void RegisterServices(Container container)
        {
            // factories
            ObjectFactory objectFactory = new ObjectFactory(container);
            container.RegisterSingleton<IObjectFactory>(objectFactory);

            // repository / eventstore
            container.Register(typeof(IRepository<>), typeof(Repository<>));

            container.Register<IEventStore, EventStore>();

            container.ResolveUnregisteredType += (s, e) =>
            {
                Type serviceType = e.UnregisteredServiceType;

                if (serviceType.IsGenericType &&
                    serviceType.GetGenericTypeDefinition() == typeof(IRepository<>))
                {
                    Type implementationType = typeof(Repository<>)
                        .MakeGenericType(serviceType.GetGenericArguments()[0]);

                    Registration r = Lifestyle.Transient.CreateRegistration(
                        serviceType,
                        () => Activator.CreateInstance(implementationType),
                        container);

                    e.Register(r);
                }
            };
        }

        public static void RegisterEventBus(Container container)
        {
            container.RegisterSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
            container.RegisterSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            container.RegisterSingleton<IConnectionFactory, ConnectionFactory>();
            container.RegisterSingleton<IIntegrationEventBus, EventBusRabbitMQ>();
        }

        public static void RegisterEventForwarder(Container container)
        {
            var typesToRegister = container.GetTypesToRegister(
                                                       typeof(ICommittedEventHandler<>),
                                                       new[] { typeof(ImmediateEventForwarder).Assembly },
                                                       new TypesToRegisterOptions
                                                       {
                                                           IncludeGenericTypeDefinitions = true,
                                                           IncludeComposites = false,
                                                       });

            typesToRegister = new[] { typeof(ImmediateEventForwarder) };

            container.RegisterCollection(typeof(ICommittedEventHandler<>), typesToRegister);
        }
    }
}