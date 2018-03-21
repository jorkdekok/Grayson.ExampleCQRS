using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate;
using MongoDB.Bson.Serialization;

namespace Grayson.ExampleCQRS.Infrastructure.EventSourcing
{
    public static class EventMappings
    {
        private static bool isConfigured = false;

        private static object myLock = new object();

        public static void Configure()
        {
            lock (myLock)
            {
                if (isConfigured)
                {
                    return; // skip configuration
                }

                // generic event classes
                AutoMapClass<EventStream>();
                AutoMapClass<EventWrapper>();

                // Rit events
                AutoMapClass<RitCreated>();
                AutoMapClass<RitUpdated>();

                // KmStand events
                AutoMapClass<KmStandCreated>();
                AutoMapClass<KmStandUpdated>();

                AutoMapClass<SnapshotWrapper>();

                isConfigured = true;
            }
        }

        private static void AutoMapClass<T>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
                BsonClassMap.RegisterClassMap<T>(cm => cm.AutoMap());
        }
    }
}