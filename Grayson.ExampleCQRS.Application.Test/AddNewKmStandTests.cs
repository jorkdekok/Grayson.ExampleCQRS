using System;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;
using Grayson.Utils.DDD.Infrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Application.Test
{
    [TestClass]
    public class AddNewKmStandTests
    {
        [TestMethod]
        public void AddNewKmStandTest1()
        {
            Container container = new Container();
            container.Options.AllowResolvingFuncFactories();

            container.RegisterSingleton<ICommandBus>(new SimpleBus(container));

            ObjectFactory objectFactory = new ObjectFactory(container);
            container.RegisterSingleton<IObjectFactory>(objectFactory);

            var eventPublisher = new EventPublisher(objectFactory);
            container.RegisterSingleton<IEventPublisher>(eventPublisher);

            container.Register<IAggregateFactory, AggregateFactory>();

            //var t = typeof(KmStand);
            //container.Register<KmStand>();
            RepositoryRegistrations.Register(container);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // register command handlers
            container.Register(typeof(ICommandHandler<>), assemblies);
            container.RegisterCollection(typeof(IDomainEventHandler<>), assemblies);

            ICommandBus bus = container.GetInstance<ICommandBus>();
            container.GetInstance<IRepository<KmStand>>();

            bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));
        }
    }
}