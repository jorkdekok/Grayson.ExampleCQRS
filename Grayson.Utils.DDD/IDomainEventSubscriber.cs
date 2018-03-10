namespace Grayson.Utils.DDD
{
    public interface IDomainEventSubscriber<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        void When(TDomainEvent @event);
    }
}