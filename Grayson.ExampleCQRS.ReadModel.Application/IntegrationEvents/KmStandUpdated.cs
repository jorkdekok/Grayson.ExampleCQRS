using Grayson.SeedWork.DDD.Application.Integration;

using System;

namespace Grayson.ExampleCQRS.ReadModel.Application.IntegrationEvents
{
    public class KmStandUpdated : IntegrationEvent
    {
        public Guid AdresId { get; private set; }
        public DateTime Datum { get; private set; }
        public Guid Id { get; private set; }
        public int Stand { get; private set; }

        public KmStandUpdated(Guid id, int stand, DateTime datum, Guid adresId)
        {
            Id = id;
            Stand = stand;
            Datum = datum;
            AdresId = adresId;
        }
    }
}