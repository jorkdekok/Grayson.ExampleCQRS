using System;

using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class RitCreated : IDomainEvent
    {
        public readonly string Name;
        public Guid Id { get; set; }

        public RitCreated(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}