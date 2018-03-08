using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using SimpleInjector;
using System;

namespace Grayson.ExampleCQRS.Infrastructure.Repository
{
    public static class RepositoryRegistrations
    {
        public static void Register(Container container)
        {
            var assemblies = new[] { typeof(Rit).Assembly };
            container.RegisterCollection(typeof(Rit), assemblies);

            container.Register(typeof(IRepository<>), typeof(Repository<>));

            container.Register<IEventStore, EventStore>();
        }
    }
}