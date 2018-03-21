using System.Threading.Tasks;

namespace Grayson.SeedWork.DDD.Application
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task When(TCommand command);
    }
}