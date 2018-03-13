using System;
using System.Threading.Tasks;
using Grayson.Utils.DDD;

using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class AdvancedBus : IMessgeBus
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

        public async void Publish<T>(T @event)
            where T : class, IDomainEvent
        {
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}" + $"{RabbitMqConstants.EventsQueue}");

            await _bus.Publish(@event, @event.GetType());
        }

        public void RegisterHandler<TCommandHandler, TInstance>()
        {
        }

        public async void Send<T>(T command)
            where T : class, ICommand
        {
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}" +
                $"{RabbitMqConstants.CommandsQueue}");
            var endPoint = await _bus.GetSendEndpoint(sendToUri);

            await endPoint
                .Send(command);
        }
    }
}