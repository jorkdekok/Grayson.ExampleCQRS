using System;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate
{
    public interface IRitRepository : IRepository<Rit>
    {
        Rit FindByKmStandId(Guid kmStandId);
    }
}
