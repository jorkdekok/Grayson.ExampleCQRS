using Grayson.Utils.DDD;
using NetMQ.Sockets;
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

        private static IList<Type> _registeredHandlers = new List<Type>();

        public SimpleBus()
        {
            _server = new ResponseSocket("@tcp://127.0.0.1:5556");
            _client = new RequestSocket(">tcp://127.0.0.1:5556");
        }

        public void RegisterHandler<T>()
        {
            _registeredHandlers.Add(typeof(T));
        }

        public void Send<T>(T command) where T : ICommand
        {
            if (_registeredHandlers.Contains(typeof(T)))
            {
                ICommandHandler<T> handler = _registeredHandlers.Where(t => t == typeof(T)).Single() as ICommandHandler<T>;
                handler.Handle(command);
            }
        }
    }
}
