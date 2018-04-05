using RabbitMQ.Client;
using System;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
