namespace Grayson.Utils.DDD
{
    public interface IDomainEventSubscriber<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        void On(TDomainEvent @event);
    }
}