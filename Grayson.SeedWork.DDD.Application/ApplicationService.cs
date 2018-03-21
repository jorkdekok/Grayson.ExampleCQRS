using System;
using System.Threading.Tasks;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.SeedWork.DDD.Application
{
    public class ApplicationService
    {
        protected async Task Update<TAggregate>(
            Guid id,
            Func<IRepository<TAggregate>> repositoryFactory,
            Action<TAggregate> execute)
        {
            var repository = repositoryFactory();

            TAggregate aggregate = await repository.FindBy(id);
            execute(aggregate);
            await repository.Update(aggregate);
        }
    }
}