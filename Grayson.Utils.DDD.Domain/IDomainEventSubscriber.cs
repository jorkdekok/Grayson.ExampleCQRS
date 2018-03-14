namespace Grayson.Utils.DDD.Domain
{
    public interface IDomainEventSubscriber<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        void When(TDomainEvent @event);
    }
}