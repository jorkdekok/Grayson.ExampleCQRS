using System;

using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Ritten.Application.Commands;
using Grayson.ExampleCQRS.Ritten.Application.Services;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.Registrations
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
            var assemblies = new[] { typeof(AddNewRit).Assembly };
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
            var assemblies = new[] { typeof(Rit).Assembly, typeof(KmStand).Assembly };
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