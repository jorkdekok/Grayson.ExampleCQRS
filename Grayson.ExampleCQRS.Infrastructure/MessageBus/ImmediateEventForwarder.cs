using Grayson.SeedWork.DDD.Domain;

using MassTransit;

using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class ImmediateEventForwarder : ICommittedEventHandler<IDomainEvent>
    {
        private readonly IBusControl _bus;
        private readonly ILogger _logger;

        public ImmediateEventForwarder(ILogger logger, IBusControl bus)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task When(IDomainEvent @event)
        {
            _logger.LogInformation($"Sending event: {@event.GetType()}");
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}" + $"{RabbitMqConstants.EventsQueue}");
            // TODO: convert to DTO (external event)
            await _bus.Publish(@event, @event.GetType());
        }
    }
}