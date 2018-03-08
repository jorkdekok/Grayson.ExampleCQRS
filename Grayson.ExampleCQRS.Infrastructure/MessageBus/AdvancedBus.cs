using Grayson.Utils.DDD;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class AdvancedBus : IServiceBus
    {

        public AdvancedBus()
        {
            
        }

        public void Configure()
        {

        }

        public void RegisterHandler<TCommandHandler, TInstance>()
        {
            
        }

        public void Send<T>(T command) where T : ICommand
        {
            
        }
    }
}
