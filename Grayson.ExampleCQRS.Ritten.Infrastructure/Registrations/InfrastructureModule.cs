using Grayson.ExampleCQRS.Infrastructure;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.Repository;
using Grayson.ExampleCQRS.Ritten.Infrastructure.ReadModel.Repository;
using Grayson.SeedWork.DDD.Domain;

using Microsoft.EntityFrameworkCore.Design;

using SimpleInjector;

using System;
using System.Linq;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.Registrations
{
    public static class InfrastructureModule
    {
        public static void RegisterAll(Container container)
        {
            // factories
            ObjectFactory objectFactory = new ObjectFactory(container);
            container.RegisterSingleton<IObjectFactory>(objectFactory);

            container.Register(typeof(IRitRepository), typeof(RitRepository));

            // repository / eventstore
            container.Register(typeof(IRepository<>), typeof(Repository<>));

            container.Register<IEventStore, EventStore>();

            RegisterDbContext(container);
            RegisterRepositories(container);

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

        public static void RegisterRepositories(Container container)
        {
            var repositoryAssembly = typeof(KmStandViewRepository).Assembly;

            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace == "Grayson.ExampleCQRS.Ritten.Infrastructure.ReadModel.Repository"
                where type.GetInterfaces().Any()
                select new
                {
                    Service = type.GetInterfaces().Where(i => i.Name.EndsWith("Repository", System.StringComparison.Ordinal)).SingleOrDefault(),
                    Implementation = type
                };

            foreach (var reg in registrations)
            {
                if (reg.Service != null)
                {
                    container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);
                }
            }
        }

        private static void RegisterDbContext(Container container)
        {
            container.Register<IDesignTimeDbContextFactory<ReadModelDbContext>, ReadModelDbContextFactory>();
        }
    }
}