using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.ReadModel.Projections
{
    public class KmStandProjectionService : ApplicationService,
        IDomainEventHandler<KmStandCreated>,
        IDomainEventHandler<KmStandUpdated>
    {
        private readonly IKmStandViewRepository _kmStandRepository;

        public KmStandProjectionService(IKmStandViewRepository kmStandRepository)
        {
            _kmStandRepository = kmStandRepository;
        }

        public void When(KmStandCreated @event)
        {
            var kmstandView = _kmStandRepository.GetById(@event.Id);
            if (kmstandView == null)
            {
                _kmStandRepository.Add(new KmStandView() { Id = @event.Id, AdresId = @event.AdresId, Datum = @event.Datum, Stand = @event.Stand });
                _kmStandRepository.SaveChanges();
            }
        }

        public void When(KmStandUpdated @event)
        {
            var kmstandView = _kmStandRepository.GetById(@event.Id);
            if (kmstandView != null)
            {
                kmstandView.AdresId = @event.AdresId;
                kmstandView.Datum = @event.Datum;
                kmstandView.Stand = @event.Stand;

                _kmStandRepository.SaveChanges();
            }
        }
    }
}