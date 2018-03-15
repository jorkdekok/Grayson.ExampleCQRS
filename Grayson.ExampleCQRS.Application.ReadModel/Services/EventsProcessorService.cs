using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.ReadModel.Services
{
    public class EventsProcessorService : ApplicationService, IDomainEventHandler<KmStandCreated>
    {
        public void When(KmStandCreated @event)
        {
        }
    }
}