using System;
using System.Threading.Tasks;

using Grayson.Utils.DDD;

using MassTransit;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class MassTransitEventConsumer<TRequest> : IConsumer<TRequest>
        where TRequest : class
    {
        private readonly Container _container;

        public MassTransitEventConsumer(Container container)
        {
            _container = container;
        }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            await Console.Out.WriteLineAsync($"Received event message: {context.Message.GetType()}");

            Type messageType = context.Message.GetType();
            Type eventSubscriverType = typeof(IDomainEventSubscriber<>);
            Type constructedType = eventSubscriverType.MakeGenericType(messageType);

            var subscribers = _container.GetAllInstances(constructedType);
            foreach (var subscriber in subscribers)
            {
                ((dynamic)subscriber).On(context.Message);
            }
        }
    }
}