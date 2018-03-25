using System;

namespace Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Repository
{
    public interface IRepository<TAggregate>
    {
        TAggregate GetById(Guid id);

        void Add(TAggregate aggregate);

        void Update(TAggregate aggregate);

        void Delete(TAggregate aggregate);

        void SaveChanges();
    }
}