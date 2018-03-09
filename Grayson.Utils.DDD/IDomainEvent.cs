using System;

namespace Grayson.Utils.DDD
{
    public interface IDomainEvent
    {
        Guid Id { get; }
    }
}