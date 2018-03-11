using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.ReadModel.Domain.Model;
using Grayson.ExampleCQRS.ReadModel.Domain.Repository;
using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.ReadModel.Application.Projections
{
    public class KmStandProjectionService : ApplicationService, IDomainEventSubscriber<KmStandCreated>
    {
        private readonly IKmStandRepository _kmStandRepository;

        public KmStandProjectionService(IKmStandRepository kmStandRepository)
        {
            _kmStandRepository = kmStandRepository;
        }

        public void When(KmStandCreated @event)
        {
            _kmStandRepository.Add(new KmStandView { Id = @event.Id, AdresId = @event.AdresId, Datum = @event.Datum, Stand = @event.Stand });
        }
    }
}