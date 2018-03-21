using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.ReadModel.Services
{
    public class EventsProcessorService : ApplicationService, IDomainEventHandler<KmStandCreated>
    {
        public void When(KmStandCreated @event)
        {
        }
    }
}