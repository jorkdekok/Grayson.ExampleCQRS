using Grayson.Utils.DDD.Application;

namespace Grayson.Utils.DDD.Domain
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : class, IDomainEvent;

        //void RegisterSaga<T>() where T : Saga;
        void RegisterHandler<TCommandHandler, TInstance>();

        void Send<T>(T command) where T : class, ICommand;
    }
}