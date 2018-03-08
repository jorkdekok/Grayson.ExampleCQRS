using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.Utils.DDD
{

    public interface IServiceBus
    {
        void Send<T>(T command) where T : class, ICommand;
        //void RaiseEvent<T>(T theEvent) where T : Event;
        //void RegisterSaga<T>() where T : Saga;
        void RegisterHandler<TCommandHandler, TInstance>();

        void Publish<T>(T @event) where T : class, IDomainEvent;
    }
}
