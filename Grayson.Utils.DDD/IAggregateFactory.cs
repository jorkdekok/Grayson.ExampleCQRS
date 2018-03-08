using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.Utils.DDD
{
    public interface IAggregateFactory
    {
        TAggregate Create<TAggregate>() where TAggregate : class;
    }
}
