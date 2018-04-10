using System.Collections.Generic;

namespace Grayson.SeedWork.DDD.Domain
{
    public abstract class EventSourcedAggregate : Entity, IEventSourcedAggregate
    {
        private readonly List<IDomainEvent> _changes;
        private IEventPublisher _eventPublisher;

        public int Version { get; set; }

        public EventSourcedAggregate()
        {
            _changes = new List<IDomainEvent>();
        }

        public EventSourcedAggregate(IEventPublisher eventPublisher) : this()
        {
            _eventPublisher = eventPublisher;
        }

        public void AddChange(IDomainEvent @event)
        {
            _changes.Add(@event);
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
                _eventPublisher?.Publish(@event);
                _eventPublisher?.PublishCommitted(@event);
            }
            
            _changes.Clear();
        }

        public void SetEventPublisher(IEventPublisher eventPublisher)
        {
            if (_eventPublisher == null)
            {
                _eventPublisher = eventPublisher;
            }
        }
    }
}