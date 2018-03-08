using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class Adres : EventSourcedAggregate
    {
        public int Huisnummer { get; private set; }

        public string Postcode { get; private set; }

        public Adres(IServiceBus bus) : base(bus)
        {
        }

        private Adres()
        {
        }
    }
}