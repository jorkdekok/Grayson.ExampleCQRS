using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.Infrastructure.EventSourcing
{
    public class EventWrapper
    {
        public IDomainEvent Event { get; private set; }
        public int EventNumber { get; private set; }
        public string EventStreamId { get; private set; }
        public string Id { get; private set; }

        public EventWrapper(IDomainEvent @event, int eventNumber, string streamStateId)
        {
            Event = @event;
            EventNumber = eventNumber;
            EventStreamId = streamStateId;
            Id = string.Format("{0}-{1}", streamStateId, EventNumber);
        }
    }
}