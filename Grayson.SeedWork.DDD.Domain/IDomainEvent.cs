using System;

namespace Grayson.SeedWork.DDD.Domain
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        DateTime OccurredOn { get; set; }
    }
}