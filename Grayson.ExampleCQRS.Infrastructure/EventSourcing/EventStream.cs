using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Infrastructure.EventSourcing
{
    public class EventStream
    {
        public string Id { get; private set; } //aggregate type + id
        public int Version { get; private set; }

        public EventStream(string id)
        {
            Id = id;
            Version = 0;
        }

        private EventStream()
        {
        }

        public EventWrapper RegisterEvent(IDomainEvent @event)
        {
            Version++;

            return new EventWrapper(@event, Version, Id);
        }
    }
}