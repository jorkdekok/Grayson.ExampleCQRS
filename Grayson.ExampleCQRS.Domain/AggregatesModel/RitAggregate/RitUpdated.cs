using System;

using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate
{
    public class RitUpdated : IDomainEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public RitUpdated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}