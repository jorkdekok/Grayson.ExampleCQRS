using System;

namespace Grayson.SeedWork.DDD.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public int Version;

        public Guid Id { get; set; }

        public DateTime OccurredOn { get; set; }

        public DomainEvent()
        {
            OccurredOn = DateTime.Now;
        }
    }
}