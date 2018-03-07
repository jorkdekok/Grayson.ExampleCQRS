using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.Utils.DDD
{
    public interface IApplyEvent<in TEvent>
        where TEvent : IDomainEvent

    {
        void Apply(TEvent @event);
    }
}
