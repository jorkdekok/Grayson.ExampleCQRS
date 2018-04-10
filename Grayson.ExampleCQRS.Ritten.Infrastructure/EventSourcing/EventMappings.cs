using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;

using MongoDB.Bson.Serialization;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.EventSourcing
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

                // Rit events
                AutoMapClass<RitCreated>();
                AutoMapClass<RitUpdated>();

                if (IsMapped<Rit>())
                {
                    BsonClassMap.RegisterClassMap<Rit>(cm =>
                    {
                        cm.AutoMap();
                    });
                }

                isConfigured = true;
            }
        }

        private static void AutoMapClass<T>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
                BsonClassMap.RegisterClassMap<T>(cm => cm.AutoMap());
        }

        private static bool IsMapped<T>()
        {
            return !BsonClassMap.IsClassMapRegistered(typeof(T));
        }
    }
}