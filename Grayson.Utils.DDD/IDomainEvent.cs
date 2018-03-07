using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.Utils.DDD
{
    public interface IDomainEvent
    {
        Guid Id { get; }
    }
}
