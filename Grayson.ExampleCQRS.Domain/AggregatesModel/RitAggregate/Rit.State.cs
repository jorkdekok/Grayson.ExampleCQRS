using System;
using System.Diagnostics;

using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate
{
    public partial class Rit : EventSourcedAggregate,
        IApplyEvent<RitCreated>,
        IApplyEvent<RitUpdated>
    {
        public int BeginStand { get; set; }
        public Guid BeginStandId { get; set; }
        public int EindStand { get; set; }
        public Guid EindStandId { get; set; }
        public string Name { get; private set; }

        public Rit(IEventPublisher serviceBus) : base(serviceBus)
        {
        }

        public void Apply(RitCreated @event)
        {
            this.Id = @event.Id;
            this.Name = @event.Name;
            this.BeginStand = @event.BeginStand;
            this.BeginStandId = @event.BeginStandId;
            this.EindStand = @event.EindStand;
            this.EindStandId = @event.EindStandId;

            Debug.WriteLine("Event apply");
        }

        public void Apply(RitUpdated @event)
        {
            this.Name = @event.Name;
            this.BeginStand = @event.BeginStand;
            this.BeginStandId = @event.BeginStandId;
            this.EindStand = @event.EindStand;
            this.EindStandId = @event.EindStandId;
        }
    }
}