using System;
using System.Collections.Generic;

using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class SimpleBus : IEventPublisher
    {
        private static IList<Type> _registeredHandlers = new List<Type>();
        private readonly Container _container;

        public SimpleBus()
        {
            _container = new Container();

            MessageBusRegistrations.Register(_container);
        }

        public void Publish<T>(T @event)
            where T : class, IDomainEvent
        {
            throw new NotImplementedException();
        }

        public void RegisterHandler<TCommandHandler, TInstance>()
        {
            _container.Register(typeof(TCommandHandler), typeof(TInstance));
        }

        public void Send<T>(T command) where T : class, ICommand
        {
            var instance = _container.GetInstance<ICommandHandler<T>>();
            instance.When(command);
        }
    }
}