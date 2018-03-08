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
            container.Register(typeof(IRepository<>), typeof(Repository<>));

            container.Register<IEventStore, EventStore>();
        }
    }
}