﻿using System;

using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class KmStandUpdated : IDomainEvent
    {
        public Guid AdresId { get; private set; }
        public DateTime Datum { get; private set; }
        public Guid Id { get; set; }
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