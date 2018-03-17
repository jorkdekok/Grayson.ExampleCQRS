namespace Grayson.SeedWork.DDD.Domain
{
    public interface IAggregateFactory
    {
        TAggregate Create<TAggregate>() where TAggregate : class;
    }
}