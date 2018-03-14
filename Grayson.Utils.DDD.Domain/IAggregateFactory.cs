namespace Grayson.Utils.DDD.Domain
{
    public interface IAggregateFactory
    {
        TAggregate Create<TAggregate>() where TAggregate : class;
    }
}