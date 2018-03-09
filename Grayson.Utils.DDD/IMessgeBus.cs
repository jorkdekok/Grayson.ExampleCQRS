namespace Grayson.Utils.DDD
{
    public interface IMessgeBus
    {
        void Publish<T>(T @event) where T : class, IDomainEvent;

        //void RaiseEvent<T>(T theEvent) where T : Event;
        //void RegisterSaga<T>() where T : Saga;
        void RegisterHandler<TCommandHandler, TInstance>();

        void Send<T>(T command) where T : class, ICommand;
    }
}