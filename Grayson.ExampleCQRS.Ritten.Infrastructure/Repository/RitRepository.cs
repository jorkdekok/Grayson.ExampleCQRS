using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.SeedWork.DDD.Domain;

using System;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.Repository
{
    public class RitRepository : Repository<Rit>, IRitRepository
    {
        public RitRepository(
            IAggregateFactory aggregateFactory,
            IEventStore eventStore,
            IEventPublisher eventPublisher) : base(aggregateFactory, eventStore, eventPublisher)
        {
        }

        public Rit FindByKmStandId(Guid kmStandId)
        {
            throw new NotImplementedException();
        }
    }
}