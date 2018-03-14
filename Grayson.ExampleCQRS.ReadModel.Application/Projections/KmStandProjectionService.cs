using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.ReadModel.Domain.Model;
using Grayson.ExampleCQRS.ReadModel.Domain.Repository;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.ReadModel.Application.Projections
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