using System;

using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate
{
    public class KmStandCreated : DomainEvent, IDomainEvent
    {
        public Guid AdresId { get; private set; }
        public DateTime Datum { get; private set; }
        public int Stand { get; private set; }

        public KmStandCreated(Guid id, int stand, DateTime datum, Guid adresId)
        {
            Id = id;
            Stand = stand;
            Datum = datum;
            AdresId = adresId;
        }
    }
}