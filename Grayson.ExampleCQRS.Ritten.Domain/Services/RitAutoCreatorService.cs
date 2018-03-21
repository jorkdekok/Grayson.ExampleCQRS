using System;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Ritten.Domain.Services
{
    public class RitAutoCreatorService : DomainService
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly SeedWork.DDD.Domain.IRepository<KmStand> _kmStandRepository;
        private readonly IKmStandViewRepository _kmStandViewRepository;
        private readonly SeedWork.DDD.Domain.IRepository<Rit> _ritRepository;
        private readonly IRitViewRepository _ritViewRepository;

        public RitAutoCreatorService(
            IAggregateFactory aggregateFactory,
            IRitViewRepository ritViewRepository,
            IKmStandViewRepository kmStandViewRepository,
            SeedWork.DDD.Domain.IRepository<KmStand> kmStandRepository,
            SeedWork.DDD.Domain.IRepository<Rit> ritRepository)
        {
            _aggregateFactory = aggregateFactory;
            _ritRepository = ritRepository;
            _kmStandRepository = kmStandRepository;
            _kmStandViewRepository = kmStandViewRepository;
            _ritViewRepository = ritViewRepository;
        }

        public void AutoCreateRitWhenNeeded(KmStandCreated @event)
        {
            // zoek laatste kmstand
            var standView = _kmStandViewRepository.GetLastOne();
            if (standView != null)
            {
                // is deze al gekoppeld aan een rit als eind stand?
                var ritView = _ritViewRepository.FindByLastKmStandId(@event.Id);
                if (ritView == null)
                {
                    // zo nee, dan rit aanmaken en de kmstandid koppelen als begin stand
                    Rit rit = _aggregateFactory.Create<Rit>();
                    rit.Create("Generated", standView.Stand, standView.Id, 0, Guid.Empty, Guid.NewGuid());
                    _ritRepository.Add(rit);
                }
            }
        }
    }
}