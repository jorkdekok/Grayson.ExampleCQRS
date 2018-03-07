using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.Utils.DDD
{

    public interface IBus
    {
        void Send<T>(T command) where T : ICommand;
        //void RaiseEvent<T>(T theEvent) where T : Event;
        //void RegisterSaga<T>() where T : Saga;
        void RegisterHandler<TCommandHandler, TInstance>();
    }
}
