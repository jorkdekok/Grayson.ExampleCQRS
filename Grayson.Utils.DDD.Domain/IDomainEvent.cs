﻿using System;

namespace Grayson.Utils.DDD.Domain
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        DateTime OccurredOn { get; set; }
    }
}