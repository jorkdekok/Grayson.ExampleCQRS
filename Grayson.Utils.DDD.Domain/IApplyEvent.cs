namespace Grayson.Utils.DDD.Domain
{
    public interface IApplyEvent<in TEvent>
        where TEvent : IDomainEvent

    {
        void Apply(TEvent @event);
    }
}