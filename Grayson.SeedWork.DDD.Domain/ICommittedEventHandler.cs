using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.SeedWork.DDD.Domain
{
    public interface ICommittedEventHandler<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        Task When(TDomainEvent @event);
    }
}
