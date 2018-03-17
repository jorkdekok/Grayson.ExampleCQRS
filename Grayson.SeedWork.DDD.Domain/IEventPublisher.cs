namespace Grayson.SeedWork.DDD.Domain
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : class, IDomainEvent;

        void PublishCommitted<T>(T @event) where T : class, IDomainEvent;

    }
}