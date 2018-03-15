using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.Utils.DDD.Domain
{
    public interface ICommittedEventHandler<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        void When(TDomainEvent @event);
    }
}
