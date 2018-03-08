using Grayson.Utils.DDD;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

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
            where TAggregate: class
        {
            return container.GetInstance<TAggregate>();   
        }
    }
}
