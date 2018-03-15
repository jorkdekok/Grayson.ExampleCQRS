using System;

using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class AdvancedBus : ICommandBus, IDomainEventHandler<IDomainEvent>
    {
        private readonly IBusControl _bus;

        public AdvancedBus(IBusControl bus)
        {
            _bus = bus;
        }

        public static IBusControl ConfigureBus(
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
                registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(RabbitMqConstants.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqConstants.UserName);
                    hst.Password(RabbitMqConstants.Password);
                });

                registrationAction?.Invoke(cfg, host);
            });
        }

        public void Configure()
        {
        }

        public async void Send<T>(T command)
            where T : class, ICommand
        {
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}" +
                $"{RabbitMqConstants.CommandsQueue}");
            var endPoint = await _bus.GetSendEndpoint(sendToUri);
            // TODO: convert to DTO
            await endPoint
                .Send(command);
        }

        public void When(IDomainEvent @event)
        {
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}" + $"{RabbitMqConstants.EventsQueue}");
            // TODO: convert to DTO (external event)
            _bus.Publish(@event, @event.GetType());
        }
    }
}