using System;

using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate
{
    public partial class Rit : EventSourcedAggregate, IApplyEvent<RitCreated>, IApplyEvent<RitUpdated>
    {
        public void Create(string name, int beginStand, Guid beginStandId, int eindStand, Guid eindStandId, Guid id)
        {
            this.Causes(new RitCreated(name, beginStand, beginStandId, eindStand, eindStandId, id));
        }

        public void Update(string name, int beginStand, Guid beginStandId, int eindStand, Guid eindStandId, Guid id)
        {
            this.Causes(new RitUpdated(beginStand, beginStandId, eindStand, eindStandId, id, name));
        }
    }
}