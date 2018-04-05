using System.Threading.Tasks;

namespace Grayson.SeedWork.DDD.Application.Integration
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task When(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}