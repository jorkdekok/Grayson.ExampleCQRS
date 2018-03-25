using Grayson.SeedWork.DDD.Application;

using MassTransit;

using Microsoft.Extensions.Logging;

using SimpleInjector;

using System;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class MassTransitCommandHandler<TRequest> : IConsumer<TRequest>
        where TRequest : class
    {
        private readonly Container _container;
        private readonly ILogger _logger;

        public MassTransitCommandHandler(ILogger logger, Container container)
        {
            _container = container;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            try
            {
                _logger.LogInformation($"Received command message: {context.Message.GetType()}");

                Type messageType = context.Message.GetType();
                Type commandhandlerType = typeof(ICommandHandler<>);
                Type constructedType = commandhandlerType.MakeGenericType(messageType);

                var handler = _container.GetInstance(constructedType);
                ((dynamic)handler).When(context.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
        }
    }
}