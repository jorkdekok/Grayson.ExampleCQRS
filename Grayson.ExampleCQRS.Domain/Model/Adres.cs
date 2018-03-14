using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class Adres : EventSourcedAggregate
    {
        public int Huisnummer { get; private set; }

        public string Postcode { get; private set; }

        public Adres(IMessgeBus serviceBus) : base(serviceBus)
        {
        }
    }
}