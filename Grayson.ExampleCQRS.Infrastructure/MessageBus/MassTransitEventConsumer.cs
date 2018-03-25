using Grayson.SeedWork.DDD.Domain;

using MassTransit;

using Microsoft.Extensions.Logging;

using SimpleInjector;

using System;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class MassTransitEventConsumer<TRequest> : IConsumer<TRequest>
        where TRequest : class
    {
        private readonly Container _container;
        private readonly ILogger _logger;

        public MassTransitEventConsumer(ILogger logger, Container container)
        {
            _container = container;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            try
            {
                _logger.LogInformation($"Received event message: {context.Message.GetType()}");

                Type messageType = context.Message.GetType();
                Type eventSubscriverType = typeof(IDomainEventHandler<>);
                Type constructedType = eventSubscriverType.MakeGenericType(messageType);

                var subscribers = _container.GetAllInstances(constructedType);
                foreach (var subscriber in subscribers)
                {
                    _logger.LogInformation($"Event '{context.Message.GetType().Name}' -> '{subscriber.GetType().Name}'");
                    ((dynamic)subscriber).When(context.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Exception");
            }
        }
    }
}