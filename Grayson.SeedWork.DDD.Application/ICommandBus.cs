using System.Threading.Tasks;

namespace Grayson.SeedWork.DDD.Application
{
    public interface ICommandBus
    {
       void RegisterSaga<TSage>() where TSage : Saga;

        Task Send<T>(T command)
            where T : class, ICommand;
    }
}