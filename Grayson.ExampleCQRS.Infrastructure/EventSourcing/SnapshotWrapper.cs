using System;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Infrastructure.EventSourcing
{
    public class SnapshotWrapper
    {
        public string Id { get; set; }
        public string StreamName { get; set; }
        public IEventSourcedAggregate Snapshot { get; set; }
        public DateTime Created { get; set; }
    }
}