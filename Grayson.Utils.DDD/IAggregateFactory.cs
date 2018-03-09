namespace Grayson.Utils.DDD
{
    public interface IAggregateFactory
    {
        TAggregate Create<TAggregate>() where TAggregate : class;
    }
}