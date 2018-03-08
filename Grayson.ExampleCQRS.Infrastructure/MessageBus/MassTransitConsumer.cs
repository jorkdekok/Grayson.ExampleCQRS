using MassTransit;
using System;
using System.Threading.Tasks;
using SimpleInjector;
using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class MassTransitConsumer<TRequest> : IConsumer<TRequest>
        where TRequest : class
    {
        private readonly Container _container;

        public MassTransitConsumer(Container container )
        {
            _container = container;
        }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            Type messageType = context.Message.GetType();
            Type commandhandlerType = typeof(ICommandHandler<>);
            Type constructedType= commandhandlerType.MakeGenericType(messageType);

            var handler = _container.GetInstance(constructedType);
            ((dynamic)handler).When(context.Message);

            await Console.Out.WriteLineAsync($"Received command message: {context.Message.GetType()}");
        }
    }
}