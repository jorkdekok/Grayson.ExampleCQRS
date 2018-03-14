namespace Grayson.Utils.DDD.Domain
{
    public interface IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        void When(TDomainEvent @event);
    }
}