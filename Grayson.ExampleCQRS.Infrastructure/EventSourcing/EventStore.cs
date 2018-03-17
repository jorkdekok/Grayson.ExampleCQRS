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
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<EventWrapper> _events;
        private IMongoCollection<EventStream> _eventStreams;

        public EventStore()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("GrasysonTestDB");

            _eventStreams = _database.GetCollection<EventStream>("EventStreams");
            _events = _database.GetCollection<EventWrapper>("Events");

            ConfigureMappings();
        }

        public void AddSnapshot<T>(string streamName, T snapshot)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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