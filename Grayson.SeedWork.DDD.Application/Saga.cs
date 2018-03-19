using Grayson.SeedWork.DDD.Domain;

namespace Grayson.SeedWork.DDD.Application
{
    public class Saga : EventSourcedAggregate
    {
        public Saga(IEventPublisher eventPublisher) : base(eventPublisher)
        {
        }
    }
}