using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.Utils.DDD.Domain;
using Grayson.Utils.DDD.Infrastructure;
using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.Registrations
{
    public static class InfrastructureModule
    {
        public static void RegisterAll(Container container)
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

        public static void RegisterEventForwarder(Container container)
        {
            var typesToRegister = container.GetTypesToRegister(
                                                       typeof(IDomainEventHandler<>),
                                                       new[] { typeof(ImmediateEventForwarder).Assembly },
                                                       new TypesToRegisterOptions
                                                       {
                                                           IncludeGenericTypeDefinitions = true,
                                                           IncludeComposites = false,
                                                       });

            typesToRegister = new[] { typeof(ImmediateEventForwarder) };

            container.RegisterCollection(typeof(IDomainEventHandler<>), typesToRegister);
        }
    }
}
