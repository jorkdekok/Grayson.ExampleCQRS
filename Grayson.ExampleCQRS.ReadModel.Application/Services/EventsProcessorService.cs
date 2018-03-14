using Grayson.ExampleCQRS.Domain.Model;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.ReadModel.Application.Services
{
    public class EventsProcessorService : ApplicationService, IDomainEventSubscriber<KmStandCreated>
    {
        public void When(KmStandCreated @event)
        {
        }
    }
}