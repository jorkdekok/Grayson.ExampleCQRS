using Grayson.Utils.DDD;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.SimpleInjectorIntegration;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class AdvancedBus : IServiceBus
    {
        private readonly IBusControl _bus;

        public AdvancedBus(IBusControl bus)
        {
            _bus = bus;
        }

        public void Configure()
        {

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
