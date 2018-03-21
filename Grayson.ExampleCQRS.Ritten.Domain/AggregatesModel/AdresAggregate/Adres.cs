using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.AdresAggregate
{
    public class Adres : EventSourcedAggregate
    {
        public int Huisnummer { get; private set; }

        public string Postcode { get; private set; }

        public Adres(IEventPublisher serviceBus) : base(serviceBus)
        {
        }
    }
}