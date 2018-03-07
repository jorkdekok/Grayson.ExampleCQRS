using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class KmStandCreated : IDomainEvent
    {
        public Guid Id { get; set; }
        public int Stand { get; private set; }
        public DateTime Datum { get; private set; }
        public Guid AdresId { get; private set; }

        public KmStandCreated(Guid id, int stand, DateTime datum, Guid adresId)
        {
            Id = id;
            Stand = stand;
            Datum = datum;
            AdresId = adresId;
        }
    }
}
