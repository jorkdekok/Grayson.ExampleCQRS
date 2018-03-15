namespace Grayson.Utils.DDD.Domain
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
            where TAggregate : EventSourcedAggregate

        {
            ((dynamic)aggregate).Apply((dynamic)@event);
            //RedirectToApply.InvokeEventOptional(aggregate, @event); // dynamic seems faster
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
            ((dynamic)aggregate).Apply((dynamic)@event);
            //RedirectToApply.InvokeEventOptional(aggregate, @event); // dynamic seems faster
            IEventSourcedAggregate eventSourcedAggregate = (IEventSourcedAggregate)aggregate;
            eventSourcedAggregate.Version++;
        }
    }
}