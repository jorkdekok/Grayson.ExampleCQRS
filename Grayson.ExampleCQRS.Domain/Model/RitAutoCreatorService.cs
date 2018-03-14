using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.Model
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