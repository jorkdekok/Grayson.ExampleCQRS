using Grayson.SeedWork.DDD.Application.Integration;

using System;

namespace Grayson.ExampleCQRS.ReadModel.Application.IntegrationEvents
{
    public class RitUpdated : IntegrationEvent
    {
        public int BeginStand { get; private set; }
        public Guid BeginStandId { get; private set; }
        public int EindStand { get; private set; }
        public Guid EindStandId { get; private set; }
        public Guid Id { get; private set; }
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