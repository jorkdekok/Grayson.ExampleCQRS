using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class RitAutoCreatorService : DomainService, IApplyEvent<KmStandCreated>
    {
        public void Apply(KmStandCreated @event)
        {
            RitCreator(@event);
        }

        public void RitCreator(KmStandCreated @event)
        {
            // find last kmstand check if connected to existing rit
        }
    }
}