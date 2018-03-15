using System;
using System.Linq;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Application.Services;
using Grayson.ExampleCQRS.Domain.Services;
using Grayson.ExampleCQRS.Infrastructure;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.Infrastructure.Registrations;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;
using Grayson.Utils.DDD.Infrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Application.Test
{
    [TestClass]
    public class KmStandServiceTests
    {
        [TestMethod]
        public void Send_AddNewKmStand_Command_Test1()
        {
            Container container = new Container();
            container.Options.AllowResolvingFuncFactories();
            container.Options.AllowOverridingRegistrations = true;

            ObjectFactory objectFactory = new ObjectFactory(container);
            container.RegisterSingleton<IObjectFactory>(objectFactory);

            var eventPublisher = new EventPublisher(objectFactory);
            container.RegisterSingleton<IEventPublisher>(eventPublisher);

            container.Register<IAggregateFactory, AggregateFactory>();

            InfrastructureModule.RegisterAll(container);

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

        [TestMethod]
        public void Send_UpdateKmStand_Command_Test1()
        {
            Container container = new Container();
            container.Options.AllowResolvingFuncFactories();
            container.Options.AllowOverridingRegistrations = true;

            ObjectFactory objectFactory = new ObjectFactory(container);
            container.RegisterSingleton<IObjectFactory>(objectFactory);

            var eventPublisher = new EventPublisher(objectFactory);
            container.RegisterSingleton<IEventPublisher>(eventPublisher);

            container.Register<IAggregateFactory, AggregateFactory>();

            InfrastructureModule.RegisterAll(container);

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

            bus.Send(new UpdateKmStand(Guid.Parse("d6a8eb8e-690a-4eea-94ab-d300458c4b10"), 3000, DateTime.Now, Guid.Empty));
        }
    }
}