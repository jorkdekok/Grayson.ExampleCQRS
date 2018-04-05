using Grayson.ExampleCQRS.Ritten.Application.IntegrationEvents;
using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Repository;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Application.Integration;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Ritten.Application.Projections
{
    public class KmStandProjectionService : ApplicationService,
        IIntegrationEventHandler<KmStandCreated>,
        IIntegrationEventHandler<KmStandUpdated>
    {
        private readonly IKmStandViewRepository _kmStandRepository;

        public KmStandProjectionService(IKmStandViewRepository kmStandRepository)
        {
            _kmStandRepository = kmStandRepository;
        }

        public async Task When(KmStandCreated @event)
        {
            var kmstandView = _kmStandRepository.GetById(@event.Id);
            if (kmstandView == null)
            {
                _kmStandRepository.Add(new KmStandView() { Id = @event.Id, AdresId = @event.AdresId, Datum = @event.Datum, Stand = @event.Stand });
                _kmStandRepository.SaveChanges();
            }
        }

        public async Task When(KmStandUpdated @event)
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