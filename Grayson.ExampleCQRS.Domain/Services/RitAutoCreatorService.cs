using System;

using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.Services
{
    public class RitAutoCreatorService : DomainService, IDomainEventHandler<KmStandCreated>
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly Func<SeedWork.DDD.Domain.IRepository<KmStand>> _kmStandRepositoryFactory;
        private readonly Func<IKmStandViewRepository> _kmStandViewRepositoryFactory;
        private readonly Func<SeedWork.DDD.Domain.IRepository<Rit>> _ritRepositoryFactory;
        private readonly Func<IRitViewRepository> _ritViewRepositoryFactory;

        public RitAutoCreatorService(
            IAggregateFactory aggregateFactory,
            Func<IRitViewRepository> ritViewRepositoryFactory,
            Func<IKmStandViewRepository> kmStandViewRepositoryFactory,
            Func<SeedWork.DDD.Domain.IRepository<KmStand>> kmStandRepositoryFactory,
            Func<SeedWork.DDD.Domain.IRepository<Rit>> ritRepositoryFactory)
        {
            _aggregateFactory = aggregateFactory;
            _ritViewRepositoryFactory = ritViewRepositoryFactory;
            _kmStandViewRepositoryFactory = kmStandViewRepositoryFactory;
            _kmStandRepositoryFactory = kmStandRepositoryFactory;
            _ritRepositoryFactory = ritRepositoryFactory;
        }

        public void When(KmStandCreated @event)
        {
            var ritViewRepository = _ritViewRepositoryFactory();
            // zoek laatste kmstand
            var kmStandRepository = _kmStandViewRepositoryFactory();
            var standView = kmStandRepository.GetLastOne();
            if (standView != null)
            {
                // is deze al gekoppeld aan een rit als eind stand?
                var ritView = ritViewRepository.FindByLastKmStandId(@event.Id);
                if (ritView == null)
                {
                    // zo nee, dan rit aanmaken en de kmstandid koppelen als begin stand
                    Rit rit = _aggregateFactory.Create<Rit>();
                    rit.Create("Generated", standView.Stand, standView.Id, 0, Guid.Empty, Guid.NewGuid());
                    var ritRepository = _ritRepositoryFactory();
                    ritRepository.Add(rit);
                }
            }
        }
    }
}