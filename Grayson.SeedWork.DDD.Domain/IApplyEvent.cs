namespace Grayson.SeedWork.DDD.Domain
{
    /// <summary>
    /// Apply event (or change) to the aggregate
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IApplyEvent<in TEvent>
        where TEvent : IDomainEvent

    {
        void Apply(TEvent @event);
    }
}