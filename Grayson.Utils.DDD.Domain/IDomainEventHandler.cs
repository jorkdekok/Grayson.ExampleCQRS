﻿namespace Grayson.Utils.DDD.Domain
{
    public interface IDomainEventHandler<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        void When(TDomainEvent @event);
    }
}