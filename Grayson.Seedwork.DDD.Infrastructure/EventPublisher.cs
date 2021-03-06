﻿using Grayson.SeedWork.DDD.Domain;

using System;
using System.Diagnostics;

namespace Grayson.SeedWork.DDD.Infrastructure
{
    /// <summary>
    /// Default implementation for IEventPublisher
    /// </summary>
    public class EventPublisher : IEventPublisher
    {
        private readonly IObjectFactory _objectFactory;

        public EventPublisher(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
        }

        public void Publish<T>(T @event) where T : class, IDomainEvent
        {
            // publish event to all registered eventhandlers
            Type messageType = @event.GetType();
            Type eventSubscriverType = typeof(IDomainEventHandler<>);
            Type constructedType = eventSubscriverType.MakeGenericType(messageType);

            var subscribers = _objectFactory.GetAllInstances(constructedType);

            foreach (Object subscriber in subscribers)
            {
                Debug.WriteLine($"Publish event '{messageType.Name}' to '{subscriber.GetType().Name}'");
                ((dynamic)subscriber).When((dynamic)@event);
            }
        }

        void IEventPublisher.PublishCommitted<T>(T @event)
        {
            // publish event to all registered eventhandlers
            Type messageType = @event.GetType();
            Type eventSubscriverType = typeof(ICommittedEventHandler<>);
            Type constructedType = eventSubscriverType.MakeGenericType(messageType);

            var subscribers = _objectFactory.GetAllInstances(constructedType);

            foreach (Object subscriber in subscribers)
            {
                ((dynamic)subscriber).When((dynamic)@event);
            }
        }
    }
}