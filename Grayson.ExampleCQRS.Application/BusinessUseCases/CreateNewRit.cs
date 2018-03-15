using System;
using Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate;
using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.BusinessUseCases
{
    public class CreateNewRit
    {
        private readonly Func<IRepository<Rit>> _ritRepositoryFactory;
        private readonly IAggregateFactory aggregateFactory;

        public CreateNewRit(IAggregateFactory aggregateFactory, Func<IRepository<Rit>> ritRepositoryFactory)
        {
            this.aggregateFactory = aggregateFactory;
            _ritRepositoryFactory = ritRepositoryFactory;
        }

        public void Handle()
        {
            var repository = _ritRepositoryFactory();

            Rit rit = aggregateFactory.Create<Rit>();
            rit.Create(Guid.NewGuid(), "test01");

            repository.Add(rit);
        }
    }
}