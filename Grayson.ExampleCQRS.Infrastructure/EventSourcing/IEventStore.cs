using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure.EventSourcing
{
    public interface IEventStore
    {

        void CreateNewStream(string streamName, IEnumerable<IDomainEvent> domainEvents);

        void AppendEventsToStream(string streamName, IEnumerable<IDomainEvent> domainEvents, int? expectedVersion);

        IEnumerable<IDomainEvent> GetStream(string streamName, int fromVersion, int toVersion);

        void AddSnapshot<T>(string streamName, T snapshot);

        T GetLatestSnapshot<T>(string streamName) where T : class;
    }
}
