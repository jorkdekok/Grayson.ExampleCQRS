using System;

namespace Grayson.SeedWork.DDD.Domain
{
    public interface IRepository<TAggregate>
    {
        void Add(TAggregate aggregate);

        TAggregate FindBy(Guid id);

        void Update(TAggregate aggregate);
    }
}