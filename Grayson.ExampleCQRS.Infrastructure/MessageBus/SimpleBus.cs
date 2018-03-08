using Grayson.Utils.DDD;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class SimpleBus : IServiceBus
    {
        private readonly Container _container;
        private static IList<Type> _registeredHandlers = new List<Type>();

        public SimpleBus()
        {
            _container = new Container();

            RegisterCommandHandlers.AutoRegisterCommandHandlers(_container);
        }

        

        public void RegisterHandler<TCommandHandler, TInstance>()
        {
            _container.Register(typeof(TCommandHandler), typeof(TInstance));
        }

        public void Send<T>(T command) where T : ICommand
        {
            var instance = _container.GetInstance<ICommandHandler<T>>();
            instance.When(command);
        }
    }
}
