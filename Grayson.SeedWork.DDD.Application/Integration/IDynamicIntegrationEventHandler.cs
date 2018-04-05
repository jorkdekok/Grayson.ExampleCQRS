using System.Threading.Tasks;

namespace Grayson.SeedWork.DDD.Application.Integration
{
    public interface IDynamicIntegrationEventHandler
    {
        Task When(dynamic eventData);
    }
}