using System;

namespace Grayson.Utils.DDD.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public int Version;

        public Guid Id { get; set; }
    }
}