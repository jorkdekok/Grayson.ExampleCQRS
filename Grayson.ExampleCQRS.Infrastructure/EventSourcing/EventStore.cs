using System;
using System.Collections.Generic;
using System.Linq;

using Grayson.SeedWork.DDD.Domain;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Grayson.ExampleCQRS.Infrastructure.EventSourcing
{
    public class EventStore : IEventStore
    {
        private const string DATABASE = "GraysonTestDB";
        private const string EVENTS = "Events";
        private const string EVENTSTREAMS = "EventStreams";
        private const string SNAPSHOTS = "Snapshots";

        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<EventWrapper> _events;
        private IMongoCollection<EventStream> _eventStreams;
        private IMongoCollection<SnapshotWrapper> _snapshots;

        public EventStore()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase(DATABASE);

            _eventStreams = _database.GetCollection<EventStream>(EVENTSTREAMS);
            _events = _database.GetCollection<EventWrapper>(EVENTS);
            _snapshots = _database.GetCollection<SnapshotWrapper>(SNAPSHOTS);

            ConfigureMappings();
        }

        public void AddSnapshot<T>(string streamName, T snapshot)
        {
            var wrapper = new SnapshotWrapper
            {
                StreamName = streamName,
                Snapshot = snapshot as IEventSourcedAggregate,
                Created = DateTime.Now
            };

            _snapshots.InsertOne(wrapper);
        }

        public void AppendEventsToStream(string streamName, IEnumerable<IDomainEvent> domainEvents, int? expectedVersion = null)
        {
            var stream = _eventStreams.AsQueryable().Where(e => e.Id == streamName).Single();

            foreach (var @event in domainEvents)
            {
                var e = stream.RegisterEvent(@event);
                _events.InsertOne(e);
            }

            // update eventstream version
            _eventStreams.ReplaceOne(s => s.Id == streamName, stream);
        }

        public void CreateNewStream(string streamName, IEnumerable<IDomainEvent> domainEvents)
        {
            var eventStream = new EventStream(streamName);

            _eventStreams.InsertOne(eventStream);

            AppendEventsToStream(streamName, domainEvents);
        }

        public T GetLatestSnapshot<T>(string streamName) where T : class
        {
            var latestSnapshot = _snapshots.AsQueryable()
                .Where(s => s.StreamName == streamName)
                .OrderByDescending(s => s.Created)
                .FirstOrDefault();

            if (latestSnapshot == null)
            {
                return null;
            }
            else
            {
                // unwrap snapshot
                return (T)latestSnapshot.Snapshot;
            }
        }

        public IEnumerable<IDomainEvent> GetStream(string streamName, int fromVersion, int toVersion)
        {
            var events = _events.AsQueryable().Where(
                e => e.EventStreamId == streamName
                    && e.EventNumber <= toVersion
                    && e.EventNumber >= fromVersion)
                    .Select(e => e).ToList();

            if (events.Count == 0)
            {
                return null;
            }

            var domainevents = events.Select(e => e.Event).ToList();
            return domainevents;
        }

        private void ConfigureMappings()
        {
            EventMappings.Configure();
        }
    }
}