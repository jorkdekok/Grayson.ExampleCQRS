using System;

using Grayson.SeedWork.DDD.Application;

using MassTransit;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class AdvancedBus : ICommandBus
    {
        private readonly IBusControl _bus;

        public AdvancedBus(IBusControl bus)
        {
            _bus = bus;
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
    }
}