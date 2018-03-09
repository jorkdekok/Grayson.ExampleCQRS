using System;

using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.Domain.Model
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