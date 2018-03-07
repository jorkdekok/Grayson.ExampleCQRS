using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.Utils.DDD
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}
