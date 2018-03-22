using System;
using System.Threading.Tasks;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Infrastructure.Repository
{
    public class Repository<TAggregate> : IRepository<TAggregate>
        where TAggregate : class, IEventSourcedAggregate
    {
        protected readonly IAggregateFactory _aggregateFactory;
        protected readonly IEventStore _eventStore;

        public Repository(IAggregateFactory aggregateFactory, IEventStore eventStore)
        {
            _eventStore = eventStore;
            _aggregateFactory = aggregateFactory;
        }

        public async Task Add(TAggregate aggregate)
        {
            var streamName = StreamNameFor(aggregate.Id);

            _eventStore.CreateNewStream(streamName, aggregate.GetUncommittedEvents());
            _eventStore.AddSnapshot<TAggregate>(streamName, aggregate);
            aggregate.MarkEventsAsCommitted();
        }

        public async Task<TAggregate> FindBy(Guid id)
        {
            var streamName = StreamNameFor(id);

            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;

            var snapshot = _eventStore.GetLatestSnapshot<TAggregate>(streamName);
            if (snapshot != null)
            {
                fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            }

            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber);

            TAggregate aggregate = null;
            if (snapshot != null)
            {
                aggregate = snapshot;
            }
            else
            {
                aggregate = _aggregateFactory.Create<TAggregate>();
            }

            foreach (var @event in stream)
            {
                aggregate.Replay(@event);
            }

            return aggregate;
        }

        public async Task Update(TAggregate aggregate)
        {
            var streamName = StreamNameFor(aggregate.Id);

            //var expectedVersion = GetExpectedVersion(rit.InitialVersion);
            //_eventStore.AppendEventsToStream(streamName, rit.Changes, expectedVersion);
            _eventStore.AppendEventsToStream(streamName, aggregate.GetUncommittedEvents(), 0);
            aggregate.MarkEventsAsCommitted();
        }

        private int? GetExpectedVersion(int expectedVersion)
        {
            if (expectedVersion == 0)
            {
                // first time the aggregate is stored there is no expected version
                return null;
            }
            else
            {
                return expectedVersion;
            }
        }

        protected string StreamNameFor(Guid id)
        {
            // Stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(TAggregate).Name, id);
        }
    }
}