using System;
using System.Linq;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Application.Services;
using Grayson.ExampleCQRS.Domain.Model;
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
            container.Options.AllowOverridingRegistrations = true;

            ObjectFactory objectFactory = new ObjectFactory(container);
            container.RegisterSingleton<IObjectFactory>(objectFactory);

            var eventPublisher = new EventPublisher(objectFactory);
            container.RegisterSingleton<IEventPublisher>(eventPublisher);

            container.Register<IAggregateFactory, AggregateFactory>();

            RepositoryRegistrations.Register(container);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // register command handlers
            container.Register(typeof(ICommandHandler<>), assemblies);

            var typesToRegister = container.GetTypesToRegister(
                                            typeof(IDomainEventHandler<>),
                                            new[] { typeof(RitAutoCreatorService).Assembly, typeof(KmStandService).Assembly },
                                            new TypesToRegisterOptions
                                            {
                                                IncludeGenericTypeDefinitions = true,
                                                IncludeComposites = false,
                                            });

            typesToRegister = typesToRegister.Append(typeof(SimpleBus));

            container.RegisterCollection(typeof(IDomainEventHandler<>), typesToRegister);

            container.RegisterSingleton<ICommandBus>(new SimpleBus(container));

            ICommandBus bus = container.GetInstance<ICommandBus>();

            bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));
        }
    }
}