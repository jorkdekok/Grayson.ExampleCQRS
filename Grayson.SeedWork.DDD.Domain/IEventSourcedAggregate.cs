﻿using System;
using System.Collections.Generic;

namespace Grayson.SeedWork.DDD.Domain
{
    public interface IEventSourcedAggregate
    {
        Guid Id { get; }
        int Version { get; set; }

        void AddChange(IDomainEvent @event);

        IEnumerable<IDomainEvent> GetUncommittedEvents();

        void MarkEventsAsCommitted();

        void SetEventPublisher(IEventPublisher eventPublisher);
    }
}