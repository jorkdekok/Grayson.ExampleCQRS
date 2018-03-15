using System;

using Grayson.Utils.DDD.Domain;

using MassTransit;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class ImmediateEventForwarder : ICommittedEventHandler<IDomainEvent>
    {
        private readonly IBusControl _bus;

        public ImmediateEventForwarder(IBusControl bus)
        {
            _bus = bus;
        }

        public void When(IDomainEvent @event)
        {
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}" + $"{RabbitMqConstants.EventsQueue}");
            // TODO: convert to DTO (external event)
            _bus.Publish(@event, @event.GetType());
        }
    }
}