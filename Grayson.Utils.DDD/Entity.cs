using System;

namespace Grayson.Utils.DDD
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}