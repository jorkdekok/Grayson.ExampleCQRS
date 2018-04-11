using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.SeedWork.DDD.Domain;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Polly;
using Polly.Retry;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class EventBusRabbitMQ : IIntegrationEventBus, IDisposable
    {
        private const string BROKER_NAME = "event_bus";

        private readonly ILogger _logger;
        private readonly IObjectFactory _objectFactory;
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly string _queueNameConsumer;
        private readonly int _retryCount;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private IModel _consumerChannel;
        private string _queueName;

        public EventBusRabbitMQ(
            IObjectFactory objectFactory,
            IRabbitMQPersistentConnection persistentConnection,
            ILogger logger,
            IEventBusSubscriptionsManager subsManager,
            string producerQueueNameAppSetting,
            string consumerQueueNameAppSetting)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _queueName = producerQueueNameAppSetting ?? BROKER_NAME;
            ConfigureEventBus();
            _queueNameConsumer = consumerQueueNameAppSetting;
            _consumerChannel = CreateConsumerChannel();
            _retryCount = 5; // retryCount;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            _objectFactory = objectFactory;
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex.ToString());
                });

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = @event.GetType()
                    .Name;

                ConfigureExchange(channel);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    channel.BasicPublish(exchange: _queueName,
                                     routingKey: eventName,
                                     mandatory: true,
                                     basicProperties: properties,
                                     body: body);
                });
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);
            _subsManager.AddSubscription<T, TH>();
        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent
        {
            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        private void ConfigureEventBus()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                ConfigureExchange(channel);
            }
        }

        private void ConfigureExchange(IModel channel)
        {
            channel.ExchangeDeclare(exchange: _queueName,
                                                type: "fanout");

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();
            string consumerQueueName = _queueNameConsumer;

            channel.QueueDeclare(queue: consumerQueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // generate queuename for consumer
            //queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: consumerQueueName,
                  exchange: _queueName,
                  routingKey: "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(eventName, message);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: consumerQueueName,
                                 autoAck: false,
                                 consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                                      exchange: _queueName,
                                      routingKey: eventName);
                }
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogInformation($"process event: {eventName}");
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                _logger.LogInformation($"found eventhandler for: {eventName}");
                //using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                //{
                var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    if (subscription.IsDynamic)
                    {
                        //var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                        var handler = _objectFactory.GetInstance(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                        dynamic eventData = JObject.Parse(message);
                        await handler.When(eventData);
                    }
                    else
                    {
                        var eventType = _subsManager.GetEventTypeByName(eventName);
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        //var handler = scope.ResolveOptional(subscription.HandlerType);
                        var handler = _objectFactory.GetInstance(subscription.HandlerType);
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("When").Invoke(handler, new object[] { integrationEvent });
                    }
                }
                //}
            }
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: _queueName,
                    routingKey: eventName);

                if (_subsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }
    }
}