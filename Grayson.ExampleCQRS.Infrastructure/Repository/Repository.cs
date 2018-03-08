using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure.Repository
{
    public class Repository<TAggregate> : IRepository<TAggregate>
        where TAggregate: class, IEventSourcedAggregate
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateFactory _aggregateFactory;

        public Repository(IAggregateFactory aggregateFactory, IEventStore eventStore)
        {
            _eventStore = eventStore;
            _aggregateFactory = aggregateFactory;
        }

        public void Add(TAggregate aggregate)
        {
            var streamName = StreamNameFor(aggregate.Id);

            _eventStore.CreateNewStream(streamName, aggregate.GetUncommittedEvents());
        }

        private string StreamNameFor(Guid id)
        {
            // Stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(TAggregate).Name, id);
        }

        public TAggregate FindBy(Guid id)
        {
            var streamName = StreamNameFor(id);

            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;

            //var snapshot = _eventStore.GetLatestSnapshot<PayAsYouGoAccountSnapshot>(streamName);
            //if (snapshot != null)
            //{
            //    fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            //}

            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber);

            TAggregate rit = _aggregateFactory.Create<TAggregate>();
            //if (snapshot != null)
            //{
            //    payAsYouGoAccount = new PayAsYouGoAccount(snapshot);
            //}
            //else
            //{
            //    payAsYouGoAccount = new PayAsYouGoAccount();
            //}


            foreach (var @event in stream)
            {
                rit.Replay(@event);
            }

            return rit;
        }

        public void Save(TAggregate aggregate)
        {
            var streamName = StreamNameFor(aggregate.Id);

            //var expectedVersion = GetExpectedVersion(rit.InitialVersion);
            //_eventStore.AppendEventsToStream(streamName, rit.Changes, expectedVersion);
            _eventStore.AppendEventsToStream(streamName, aggregate.GetUncommittedEvents(), 0);
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
    }
}
