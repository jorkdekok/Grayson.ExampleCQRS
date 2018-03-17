using System;

namespace Grayson.SeedWork.DDD.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}