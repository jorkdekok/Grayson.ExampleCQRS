using System;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.SeedWork.DDD.Application
{
    public class ApplicationService
    {
        protected void Update<TAggregate>(
            Guid id,
            Func<IRepository<TAggregate>> repositoryFactory,
            Action<TAggregate> execute)
        {
            var repository = repositoryFactory();

            TAggregate aggregate = repository.FindBy(id);
            execute(aggregate);
            repository.Update(aggregate);
        }
    }
}