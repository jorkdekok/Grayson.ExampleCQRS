using System;

using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public partial class KmStand : EventSourcedAggregate, IApplyEvent<KmStandCreated>
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
    }
}