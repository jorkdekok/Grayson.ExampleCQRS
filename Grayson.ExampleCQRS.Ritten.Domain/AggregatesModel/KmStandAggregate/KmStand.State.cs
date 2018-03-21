using System;

using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate
{
    public partial class KmStand : EventSourcedAggregate, 
        IApplyEvent<KmStandCreated>,
        IApplyEvent<KmStandUpdated>
    {
        public Guid AdresId { get; private set; }

        public DateTime Datum { get; private set; }

        public int Stand { get; private set; }

        public void Apply(KmStandCreated @event)
        {
            this.Id = @event.Id;
            this.Stand = @event.Stand;
            this.Datum = @event.Datum;
            this.AdresId = @event.AdresId;
        }

        public void Apply(KmStandUpdated @event)
        {
            this.Stand = @event.Stand;
            this.Datum = @event.Datum;
            this.AdresId = @event.AdresId;
        }
    }
}