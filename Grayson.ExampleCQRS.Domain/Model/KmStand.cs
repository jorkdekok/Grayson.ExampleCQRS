using System;

using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class KmStand : EventSourcedAggregate, IApplyEvent<KmStandCreated>
    {
        public Guid AdresId { get; private set; }

        public DateTime Datum { get; private set; }

        public int Stand { get; private set; }

        public KmStand(IEventPublisher serviceBus) : base(serviceBus)
        {
        }

        public void Apply(KmStandCreated @event)
        {
            this.Id = @event.Id;
            this.Stand = @event.Stand;
            this.Datum = @event.Datum;
            this.AdresId = @event.AdresId;
        }

        public void Create(int stand, DateTime datum, Guid adresId)
        {
            this.Causes(new KmStandCreated(Guid.NewGuid(), stand, datum, adresId));
        }
    }
}