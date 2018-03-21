using System;
using System.Threading.Tasks;

namespace Grayson.SeedWork.DDD.Domain
{
    public interface IRepository<TAggregate>
    {
        Task Add(TAggregate aggregate);

        Task<TAggregate> FindBy(Guid id);

        Task Update(TAggregate aggregate);
    }
}