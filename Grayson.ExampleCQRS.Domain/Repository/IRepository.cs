using System;

namespace Grayson.ExampleCQRS.Domain.Repository
{
    public interface IRepository<TAggregate>
    {
        void Add(TAggregate aggregate);

        TAggregate FindBy(Guid id);

        void Save(TAggregate aggregate);
    }
}