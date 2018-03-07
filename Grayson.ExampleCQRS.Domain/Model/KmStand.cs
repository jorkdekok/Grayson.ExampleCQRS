using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class KmStand : EventSourcedAggregate
    {
        public int Stand { get; private set; }
        public DateTime Datum { get; private set; }
        public Guid AdresId { get; private set; }
    }
}
