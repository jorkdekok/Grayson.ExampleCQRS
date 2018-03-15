using System;

namespace Grayson.Utils.DDD.Domain
{
    public interface IRepository<TAggregate>
    {
        void Add(TAggregate aggregate);

        TAggregate FindBy(Guid id);

        void Save(TAggregate aggregate);
    }
}