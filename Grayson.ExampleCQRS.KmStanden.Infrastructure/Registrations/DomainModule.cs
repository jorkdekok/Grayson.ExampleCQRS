﻿using Grayson.ExampleCQRS.Infrastructure;
using Grayson.SeedWork.DDD.Domain;
using Grayson.SeedWork.DDD.Infrastructure;

using SimpleInjector;

namespace Grayson.ExampleCQRS.KmStanden.Infrastructure.Registrations
{
    public static class DomainModule
    {
        public static void RegisterAll(Container container)
        {
            // Factories
            container.Register<IAggregateFactory, AggregateFactory>();

            // event publisher
            container.RegisterSingleton<IEventPublisher, EventPublisher>();
        }
    }
}