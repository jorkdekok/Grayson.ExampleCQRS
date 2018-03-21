using System;
using Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.BusinessUseCases
{
    public class UpdateRit
    {
        private readonly Func<IRepository<Rit>> _ritRepositoryFactory;

        public UpdateRit(IAggregateFactory aggregateFactory, Func<IRepository<Rit>> ritRepositoryFactory)
        {
            _ritRepositoryFactory = ritRepositoryFactory;
        }

        public void Handle(Guid id)
        {
            var repository = _ritRepositoryFactory();

            Rit rit = repository.FindBy(id);

            //rit.Update(id, "test02");

            repository.Update(rit);
        }
    }
}