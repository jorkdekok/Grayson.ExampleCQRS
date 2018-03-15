using Grayson.ExampleCQRS.Domain.Model;

using MongoDB.Bson.Serialization;

namespace Grayson.ExampleCQRS.Infrastructure.EventSourcing
{
    public static class EventMappings
    {
        public static void Configure()
        {
            // generic event classes
            AutoMapClass<EventStream>();
            AutoMapClass<EventWrapper>();

            // Rit events
            AutoMapClass<RitCreated>();
            AutoMapClass<RitUpdated>();

            // KmStand events
            AutoMapClass<KmStandCreated>();
            AutoMapClass<KmStandUpdated>();
        }

        private static void AutoMapClass<T>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
                BsonClassMap.RegisterClassMap<T>(cm => cm.AutoMap());
        }
    }
}