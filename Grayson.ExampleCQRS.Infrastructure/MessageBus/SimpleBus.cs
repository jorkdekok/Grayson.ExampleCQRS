using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class SimpleBus : ICommandBus, IDomainEventHandler<IDomainEvent>
    {
        private readonly Container _container;

        public SimpleBus(Container container)
        {
            _container = container;
        }

        public void Send<T>(T command) where T : class, ICommand
        {
            var instance = _container.GetInstance<ICommandHandler<T>>();
            instance.When(command);
        }

        public void When(IDomainEvent @event)
        {
        }
    }
}