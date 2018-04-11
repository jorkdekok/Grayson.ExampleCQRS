using System;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.ReadModel.Application.IntegrationEvents
{
    public class KmStandCreated : IntegrationEvent
    {
        public Guid Id { get; private set; }
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