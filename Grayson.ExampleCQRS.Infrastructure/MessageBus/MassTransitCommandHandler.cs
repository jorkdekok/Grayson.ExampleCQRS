using System;
using System.Threading.Tasks;

using Grayson.Utils.DDD.Application;

using MassTransit;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class MassTransitCommandHandler<TRequest> : IConsumer<TRequest>
        where TRequest : class
    {
        private readonly Container _container;

        public MassTransitCommandHandler(Container container)
        {
            _container = container;
        }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            try
            {
                await Console.Out.WriteLineAsync($"Received command message: {context.Message.GetType()}");

                Type messageType = context.Message.GetType();
                Type commandhandlerType = typeof(ICommandHandler<>);
                Type constructedType = commandhandlerType.MakeGenericType(messageType);

                var handler = _container.GetInstance(constructedType);
                ((dynamic)handler).When(context.Message);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Exception: {ex.Message}");
            }

        }
    }
}