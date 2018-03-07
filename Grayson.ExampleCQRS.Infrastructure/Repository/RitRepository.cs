﻿using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure.Repository
{
    public class RitRepository : IRitRepository
    {
        private readonly IEventStore _eventStore;

        public RitRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Add(Rit rit)
        {
            var streamName = StreamNameFor(rit.Id);

            _eventStore.CreateNewStream(streamName, rit.GetUncommittedEvents());
        }

        private string StreamNameFor(Guid id)
        {
            // Stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(Rit).Name, id);
        }

        public Rit FindBy(Guid id)
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

            Rit rit = new Rit();
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

        public void Save(Rit rit)
        {
            var streamName = StreamNameFor(rit.Id);

            //var expectedVersion = GetExpectedVersion(rit.InitialVersion);
            //_eventStore.AppendEventsToStream(streamName, rit.Changes, expectedVersion);
            _eventStore.AppendEventsToStream(streamName, rit.GetUncommittedEvents(), 0);
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
