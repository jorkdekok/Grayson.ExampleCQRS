using System;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.Registrations
{
    public static class RabbitMqModule
    {
        public static void RegisterAll(Container container)
        {
            container.Register<ICommandBus, AdvancedBus>();

            RegisterCommandConsumers(container);

            RegisterEventConsumers(container);
        }

        public static void RegisterCommandConsumers(Container container)
        {
            // create a generic consumer for each command and register
            var assemblies = new[] { typeof(AddNewKmStand).Assembly };
            var commands = container.GetTypesToRegister(typeof(ICommand), assemblies);

            Type mtc = typeof(MassTransitCommandHandler<>);

            foreach (var commandType in commands)
            {
                Type real = mtc.MakeGenericType(commandType);
                container.Register(real);
            }
        }

        public static void RegisterEventConsumers(Container container)
        {
            // events
            var assemblies = new[] { typeof(KmStand).Assembly };
            var events = container.GetTypesToRegister(typeof(IDomainEvent), assemblies);

            Type mte = typeof(MassTransitEventConsumer<>);

            foreach (var eventType in events)
            {
                Type real = mte.MakeGenericType(eventType);
                container.Register(real);
            }
        }
    }
}