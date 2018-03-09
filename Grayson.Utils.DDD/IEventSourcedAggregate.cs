using System;
using System.Collections.Generic;

namespace Grayson.Utils.DDD
{
    public interface IEventSourcedAggregate
    {
        Guid Id { get; }
        int Version { get; set; }

        void AddChange(IDomainEvent @event);

        IEnumerable<IDomainEvent> GetUncommittedEvents();

        void LoadsFromHistory(IEnumerable<IDomainEvent> history);

        void MarkEventsAsCommitted();
    }
}