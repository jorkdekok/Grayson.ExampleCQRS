using System;

using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate
{
    public class RitUpdated : DomainEvent, IDomainEvent
    {
        public int BeginStand { get; private set; }
        public Guid BeginStandId { get; private set; }
        public int EindStand { get; private set; }
        public Guid EindStandId { get; private set; }

        public string Name { get; private set; }

        public RitUpdated(int beginStand, Guid beginStandId, int eindStand, Guid eindStandId, Guid id, string name)
        {
            BeginStand = beginStand;
            BeginStandId = beginStandId;
            EindStand = eindStand;
            EindStandId = eindStandId;
            Id = id;
            Name = name;
        }
    }
}