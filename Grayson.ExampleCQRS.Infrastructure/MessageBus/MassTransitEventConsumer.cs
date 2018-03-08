using MassTransit;
using System;
using System.Threading.Tasks;
using SimpleInjector;
using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class MassTransitEventConsumer<TRequest> : IConsumer<TRequest>
        where TRequest : class
    {
        private readonly Container _container;

        public MassTransitEventConsumer(Container container )
        {
            _container = container;
        }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            await Console.Out.WriteLineAsync($"Received event message: {context.Message.GetType()}");
        }
    }
}