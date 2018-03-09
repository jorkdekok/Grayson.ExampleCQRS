using System.Collections.Generic;

using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.Infrastructure.EventSourcing
{
    public interface IEventStore
    {
        void AddSnapshot<T>(string streamName, T snapshot);

        void AppendEventsToStream(string streamName, IEnumerable<IDomainEvent> domainEvents, int? expectedVersion);

        void CreateNewStream(string streamName, IEnumerable<IDomainEvent> domainEvents);

        T GetLatestSnapshot<T>(string streamName) where T : class;

        IEnumerable<IDomainEvent> GetStream(string streamName, int fromVersion, int toVersion);
    }
}