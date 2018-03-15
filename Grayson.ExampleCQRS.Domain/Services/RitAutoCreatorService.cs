using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.Services
{
    public class RitAutoCreatorService : DomainService, IDomainEventHandler<KmStandCreated>
    {
        public RitAutoCreatorService()
        {

        }

        public void When(KmStandCreated @event)
        {
           
        }
    }
}