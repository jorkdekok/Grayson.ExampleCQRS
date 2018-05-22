using System;
using System.Collections.Generic;

namespace Grayson.ExampleCQRS.Domain.ReadModel.Repository
{
    public interface IRepository<TAggregate>
    {
        IEnumerable<TAggregate> GetAll(int page, int pageSize);
        
        TAggregate GetById(Guid id);

        void Add(TAggregate aggregate);

        void Update(TAggregate aggregate);

        void Delete(TAggregate aggregate);

        void SaveChanges();
    }
}