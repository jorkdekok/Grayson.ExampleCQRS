using System;

using Grayson.Utils.DDD.Domain;

namespace Grayson.Utils.DDD.Infrastructure
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
                var minfo = subscriber.GetType().GetMethod("When", new Type[] { messageType });
                minfo.Invoke(subscriber, new object[] { @event });
            }
        }
    }
}