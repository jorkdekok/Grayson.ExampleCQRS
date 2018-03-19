using System.Threading.Tasks;
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

        public void RegisterSaga<TSage>() where TSage : Saga
        {
            throw new System.NotImplementedException();
        }

        public void When(IDomainEvent @event)
        {
        }

        Task ICommandBus.Send<T>(T command)
        {
            var instance = _container.GetInstance<ICommandHandler<T>>();
            return Task.Factory.StartNew(() =>  instance.When(command));
        }
    }
}