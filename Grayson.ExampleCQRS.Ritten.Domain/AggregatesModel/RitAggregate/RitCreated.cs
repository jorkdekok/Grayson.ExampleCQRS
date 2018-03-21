using System;

using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate
{
    public class RitCreated : DomainEvent, IDomainEvent
    {
        public readonly string Name;

        public int BeginStand { get; private set; }

        public Guid BeginStandId { get; private set; }

        public int EindStand { get; private set; }

        public Guid EindStandId { get; private set; }

        public RitCreated(string name, int beginStand, Guid beginStandId, int eindStand, Guid eindStandId, Guid id)
        {
            Name = name;
            BeginStand = beginStand;
            BeginStandId = beginStandId;
            EindStand = eindStand;
            EindStandId = eindStandId;
            Id = id;
        }
    }
}