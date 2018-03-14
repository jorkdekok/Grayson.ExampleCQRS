﻿using System.Collections.Generic;

namespace Grayson.Utils.DDD.Domain
{
    public abstract class EventSourcedAggregate : Entity, IEventSourcedAggregate
    {
        private readonly IEventPublisher _bus;
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();
        private readonly IEventPublisher _serviceBus;

        public int Version { get; set; }

        public EventSourcedAggregate(IEventPublisher serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public void AddChange(IDomainEvent @event)
        {
            _changes.Add(@event);
            _serviceBus?.Publish(@event);
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _changes;
        }

        public void LoadsFromHistory(IEnumerable<IDomainEvent> history)
        {
            foreach (var e in history) ApplyEvent(e, false);
        }

        public void MarkEventsAsCommitted()
        {
            _changes.Clear();
        }

        protected void ApplyEvent(IDomainEvent @event)
        {
            ApplyEvent(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyEvent(IDomainEvent @event, bool isNew)
        {
            //this.AsDynamic().Apply(@event);
            //if (isNew) _changes.Add(@event);
        }
    }
}