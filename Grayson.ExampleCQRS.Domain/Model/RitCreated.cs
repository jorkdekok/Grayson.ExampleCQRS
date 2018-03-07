using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class RitCreated : IDomainEvent
    {
        public Guid Id { get; set; }
        public readonly string Name;

        public RitCreated(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        
    }
}
