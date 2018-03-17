using System.Collections.Generic;

namespace Grayson.SeedWork.DDD.Domain
{
    public abstract class EventSourcedAggregate : Entity, IEventSourcedAggregate
    {
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();
        private readonly IEventPublisher _eventPublisher;

        public int Version { get; set; }

        public EventSourcedAggregate(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void AddChange(IDomainEvent @event)
        {
            _changes.Add(@event);
            _eventPublisher?.Publish(@event);
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _changes;
        }

        public void MarkEventsAsCommitted()
        {
            // first publish commmitted events
            foreach (var @event in _changes)
            {
                _eventPublisher?.PublishCommitted(@event);
            }
            
            _changes.Clear();
        }
    }
}