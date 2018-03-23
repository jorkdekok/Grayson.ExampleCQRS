using System;

using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Integration.Events
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