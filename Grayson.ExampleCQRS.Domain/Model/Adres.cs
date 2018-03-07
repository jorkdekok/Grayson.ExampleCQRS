using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class Adres : EventSourcedAggregate
    {
        public string Postcode { get; private set; }
        public int Huisnummer { get; private set; }
    }
}
