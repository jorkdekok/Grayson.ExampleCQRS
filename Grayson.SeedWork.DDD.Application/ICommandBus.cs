namespace Grayson.SeedWork.DDD.Application
{
    public interface ICommandBus
    {
        //void RegisterSaga<T>() where T : Saga;
        
        void Send<T>(T command) where T : class, ICommand;
    }
}