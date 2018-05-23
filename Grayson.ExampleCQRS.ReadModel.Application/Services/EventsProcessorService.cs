using Grayson.ExampleCQRS.ReadModel.Application.IntegrationEvents;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Application.Integration;

using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Application.ReadModel.Services
{
    public class EventsProcessorService : ApplicationService, IIntegrationEventHandler<KmStandCreated>
    {
        async Task IIntegrationEventHandler<KmStandCreated>.When(KmStandCreated @event)
        {
            throw new System.NotImplementedException();
        }
    }
}