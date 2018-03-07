using Grayson.Utils.DDD;
using NetMQ.Sockets;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class SimpleBus : IBus
    {
        private readonly ResponseSocket _server;
        private readonly RequestSocket _client;
        private readonly Container _container;
        private static IList<Type> _registeredHandlers = new List<Type>();

        public SimpleBus()
        {
            _server = new ResponseSocket("@tcp://127.0.0.1:5556");
            _client = new RequestSocket(">tcp://127.0.0.1:5556");
            _container = new Container();

            AutoRegisterCommandHandlers();
        }

        private void AutoRegisterCommandHandlers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Console.WriteLine(assembly.GetName());
            }
            _container.Register(typeof(ICommandHandler<>), assemblies);
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
