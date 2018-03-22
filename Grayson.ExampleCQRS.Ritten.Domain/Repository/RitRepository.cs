using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Ritten.Domain.Repository
{
    public class RitRepository : Repository<Rit>, IRitRepository
    {
        public RitRepository(IAggregateFactory aggregateFactory, IEventStore eventStore) : base(aggregateFactory, eventStore)
        {
        }

        public Rit FindByKmStandId(Guid kmStandId)
        {
            throw new NotImplementedException();
        }
    }
}
