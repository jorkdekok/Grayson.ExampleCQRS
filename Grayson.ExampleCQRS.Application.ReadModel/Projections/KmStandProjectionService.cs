using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.ReadModel.Projections
{
    public class KmStandProjectionService : ApplicationService, IDomainEventHandler<KmStandCreated>
    {
        private readonly IKmStandRepository _kmStandRepository;

        public KmStandProjectionService(IKmStandRepository kmStandRepository)
        {
            _kmStandRepository = kmStandRepository;
        }

        public void When(KmStandCreated @event)
        {
            var kmstandView = _kmStandRepository.GetById(@event.Id);
            if (kmstandView != null)
            {
                _kmStandRepository.Add(new KmStandView { Id = @event.Id, AdresId = @event.AdresId, Datum = @event.Datum, Stand = @event.Stand });
            }
        }
    }
}