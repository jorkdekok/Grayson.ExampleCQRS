using Grayson.ExampleCQRS.Domain.Services;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD.Domain;
using Grayson.Utils.DDD.Infrastructure;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.Registrations
{
    public static class DomainModule
    {
        public static void RegisterAll(Container container)
        {
            // Factories
            container.Register<IAggregateFactory, AggregateFactory>();

            // register eventhandlers
            RegisterEventHandlers(container);

            // event publisher
            container.RegisterSingleton<IEventPublisher, EventPublisher>();
        }

        public static void RegisterEventHandlers(Container container)
        {
            var typesToRegister = container.GetTypesToRegister(
                                                       typeof(IDomainEventHandler<>),
                                                       new[] { typeof(RitAutoCreatorService).Assembly },
                                                       new TypesToRegisterOptions
                                                       {
                                                           IncludeGenericTypeDefinitions = true,
                                                           IncludeComposites = false,
                                                       });

            container.RegisterCollection(typeof(IDomainEventHandler<>), typesToRegister);

            //
            var typesToRegister2 = container.GetTypesToRegister(
                                                       typeof(ICommittedEventHandler<>),
                                                       new[] { typeof(ImmediateEventForwarder).Assembly },
                                                       new TypesToRegisterOptions
                                                       {
                                                           IncludeGenericTypeDefinitions = true,
                                                           IncludeComposites = false,
                                                       });

            container.RegisterCollection(typeof(ICommittedEventHandler<>), typesToRegister2);
        }
    }
}