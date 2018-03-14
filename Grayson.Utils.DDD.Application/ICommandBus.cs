using Grayson.Utils.DDD.Application;

namespace Grayson.Utils.DDD.Application
{
    public interface ICommandBus
    {
        //void RegisterSaga<T>() where T : Saga;
        void RegisterHandler<TCommandHandler, TInstance>();

        void Send<T>(T command) where T : class, ICommand;
    }
}