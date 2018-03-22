using System;

using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.Services;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Ritten.Application.Services
{
    public class RitService : ApplicationService,
        IDomainEventHandler<KmStandCreated>
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly Func<IRitRepository> _ritRepositoryFactory;

        public RitService(
            IAggregateFactory aggregateFactory,
            Func<IRitRepository> ritRepositoryFactory)
        {
            _aggregateFactory = aggregateFactory;
            _ritRepositoryFactory = ritRepositoryFactory;
        }

        public void When(KmStandCreated @event)
        {
            var ritRepository = _ritRepositoryFactory();

            var domainService = new RitAutoCreatorService(_aggregateFactory, ritRepository);
            domainService.AutoCreateRitWhenNeeded(@event);
        }
    }
}