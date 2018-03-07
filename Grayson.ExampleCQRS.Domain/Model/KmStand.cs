using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class KmStand : EventSourcedAggregate, IApplyEvent<KmStandCreated>
    {
        public int Stand { get; private set; }
        public DateTime Datum { get; private set; }
        public Guid AdresId { get; private set; }

        public  void Create(int stand, DateTime datum, Guid adresId)
        {
            this.Causes(new KmStandCreated(Guid.NewGuid(), stand, datum, adresId));
        }

        public void Apply(KmStandCreated @event)
        {
            this.Id = @event.Id;
            this.Stand = @event.Stand;
            this.Datum = @event.Datum;
            this.AdresId = @event.AdresId;
        }
    }
}
