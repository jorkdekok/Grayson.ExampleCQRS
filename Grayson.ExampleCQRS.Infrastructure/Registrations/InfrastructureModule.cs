using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.SeedWork.DDD.Domain;

using RabbitMQ.Client;

using SimpleInjector;

using System;

using Microsoft.Extensions.Configuration;

namespace Grayson.ExampleCQRS.Infrastructure.Registrations
{
    public static class InfrastructureModule
    {
        public static void RegisterEventBus(Container container, IConfiguration configuration)
        {
            container.RegisterSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
            container.RegisterSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            var factory = new ConnectionFactory()
            {
                HostName = configuration["EventBusConnection"]
            };

            if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
            {
                factory.UserName = configuration["EventBusUserName"];
            }

            if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
            {
                factory.Password = configuration["EventBusPassword"];
            }

            //var retryCount = 5;
            //if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
            //{
            //    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
            //}

            container.RegisterSingleton<IConnectionFactory>(factory);
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
    }
}