using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.Utils.DDD
{
    public abstract class DomainEvent : IDomainEvent
    {
        public int Version;

        public Guid Id { get; set; }
    }
}
