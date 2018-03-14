namespace Grayson.Utils.DDD.Application
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        void When(TCommand command);
    }
}