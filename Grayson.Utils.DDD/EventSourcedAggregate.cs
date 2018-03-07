using System;
using System.Collections.Generic;

namespace Grayson.Utils.DDD
{
    public abstract class EventSourcedAggregate : Entity//, IApplyEvent<IDomainEvent>
    {
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();

        public int Version { get; internal set; }

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
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<IDomainEvent> history)
        {
            foreach (var e in history) ApplyEvent(e, false);
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

        //public void Apply(IDomainEvent @event)
        //{
            
        //}
    }
}
