using System;
using System.Collections.Generic;

using Grayson.Utils.DDD.Domain;
using Grayson.Utils.DDD.Infrastructure;

namespace Grayson.Utils.DDD.Infrastructure
{
    /// <summary>
    /// Default implementation for IEventPublisher
    /// </summary>
    public class EventPublisher : IEventPublisher
    {
        private readonly IObjectFactory _objectFactory;
        private Dictionary<Type, Dictionary<Type, Type>> _handlers = new Dictionary<Type, Dictionary<Type, Type>>();

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

        public void RegisterHandler(Type eventType, Type handlerType)
        {
            if (!_handlers.ContainsKey(eventType))
            {
                Dictionary<Type, Type> handlersList = new Dictionary<Type, Type>();
                Type eventHandlerType = typeof(IDomainEventHandler<>);
                Type constructedType = eventHandlerType.MakeGenericType(eventType);
                handlersList.Add(constructedType, handlerType);
                _handlers.Add(eventType, handlersList);
            }
            else
            {
                var list = _handlers[eventType];
                Type eventHandlerType = typeof(IDomainEventHandler<>);
                Type constructedType = eventHandlerType.MakeGenericType(eventType);
                if (!list.ContainsKey(constructedType))
                {
                    list[constructedType] = handlerType;
                }
            }
        }
    }
}