﻿using System;

namespace Grayson.SeedWork.DDD.Application.Integration
{
    public interface IIntegrationEventBus : IDisposable
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;

        void UnsubscribeDynamic<TH>(string eventName)
                    where TH : IDynamicIntegrationEventHandler;
    }
}