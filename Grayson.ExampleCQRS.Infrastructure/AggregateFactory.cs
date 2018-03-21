using Grayson.SeedWork.DDD.Domain;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure
{
    public class AggregateFactory : IAggregateFactory
    {
        private readonly Container container;

        public AggregateFactory(Container container)
        {
            this.container = container;
        }

        public TAggregate Create<TAggregate>()
            where TAggregate : class
        {
            return container.GetInstance<TAggregate>();
        }
    }
}