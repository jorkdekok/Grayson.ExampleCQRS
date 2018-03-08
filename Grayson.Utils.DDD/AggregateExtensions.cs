using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Grayson.Utils.DDD
{
    public static class AggregateExtensions
    {
        /// <summary>
        /// The action in aggregate causes an event
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <param name="aggregate"></param>
        /// <param name="event"></param>
        public static void Causes<TAggregate>(this TAggregate aggregate, IDomainEvent @event)
            where TAggregate: EventSourcedAggregate

        {
            //((dynamic)aggregate).Apply((dynamic)@event);
            RedirectToWhen.InvokeEventOptional(aggregate, @event);
            EventSourcedAggregate eventSourcedAggregate = (EventSourcedAggregate)aggregate;
            eventSourcedAggregate.Version++;
            eventSourcedAggregate.AddChange(@event);
        }

        /// <summary>
        /// Replay the event to the aggregate and increase version
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <param name="aggregate"></param>
        /// <param name="event"></param>
        public static void Replay<TAggregate>(this TAggregate aggregate, IDomainEvent @event)
            where TAggregate : IEventSourcedAggregate

        {
            RedirectToWhen.InvokeEventOptional(aggregate, @event);
            IEventSourcedAggregate eventSourcedAggregate = (IEventSourcedAggregate)aggregate;
            eventSourcedAggregate.Version++;
        }
    }
}
