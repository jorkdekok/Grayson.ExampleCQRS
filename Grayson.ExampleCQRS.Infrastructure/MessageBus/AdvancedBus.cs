using System;
using System.Threading.Tasks;

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

        public void RegisterSaga<TSage>() where TSage : Saga
        {
            throw new NotImplementedException();
        }

        async Task ICommandBus.Send<T>(T command)
        {
            var sendToUri = new Uri(_bus.Address, RabbitMqConstants.CommandsQueue);
            var endPoint = await _bus.GetSendEndpoint(sendToUri);
            // TODO: convert to DTO
            await endPoint.Send(command);
        }
    }
}