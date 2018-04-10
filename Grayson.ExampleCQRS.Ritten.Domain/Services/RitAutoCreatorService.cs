using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Repository;
using Grayson.SeedWork.DDD.Domain;

using System;

namespace Grayson.ExampleCQRS.Ritten.Domain.Services
{
    public class RitAutoCreatorService : DomainService
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly IKmStandViewRepository _kmStandViewRepository;
        private readonly SeedWork.DDD.Domain.IRepository<Rit> _ritRepository;
        private readonly IRitViewRepository _ritViewRepository;

        public RitAutoCreatorService(
            IAggregateFactory aggregateFactory,
            IRitViewRepository ritViewRepository,
            IKmStandViewRepository kmStandViewRepository,
            SeedWork.DDD.Domain.IRepository<Rit> ritRepository)
        {
            _aggregateFactory = aggregateFactory;
            _ritRepository = ritRepository;
            _kmStandViewRepository = kmStandViewRepository;
            _ritViewRepository = ritViewRepository;
        }

        public void AutoCreateRitWhenNeeded(Guid id, int stand, DateTime datum, Guid adresId)
        {
            // get previous
            var prevStandView = _kmStandViewRepository.GetPrevious();

            // zoek laatste kmstand
            var lastStandView = _kmStandViewRepository.GetLastOne();
            if (prevStandView != null)
            {
                var ritViewPrevFirst = _ritViewRepository.FindByFirstKmStandId(prevStandView?.Id ?? Guid.Empty);

                // is deze gekoppeld als eerste stand aan een rit?
                var ritViewFirst = _ritViewRepository.FindByFirstKmStandId(id);

                // is deze al gekoppeld aan een rit als eind stand?
                var ritViewLast = _ritViewRepository.FindByLastKmStandId(id);

                if (ritViewPrevFirst == null && ritViewFirst == null && ritViewLast == null)
                {
                    // niet gekoppeld dan rit aanmaken en koppelen als eerste stand
                    Rit rit = _aggregateFactory.Create<Rit>();
                    rit.Create("Generated", lastStandView.Stand, lastStandView.Id, 0, Guid.Empty, Guid.NewGuid());
                    _ritRepository.Add(rit);
                }
                else
                {
                    var ritv = _ritViewRepository.FindByFirstKmStandId(prevStandView.Id);
                    // rit updaten en kmstand als laatste stand
                    Rit rit = _ritRepository.FindBy(ritv.Id).Result;
                    rit.Update(rit.Name, rit.BeginStand, rit.BeginStandId, stand, id, rit.Id);
                    _ritRepository.Update(rit);
                }
            }
            else
            { // eerste stand
                // zo nee, dan rit aanmaken en de kmstandid koppelen als begin stand
                Rit rit = _aggregateFactory.Create<Rit>();
                rit.Create("First Generated", lastStandView.Stand, lastStandView.Id, 0, Guid.Empty, Guid.NewGuid());
                _ritRepository.Add(rit);
            }
        }
    }
}