using System;

using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Ritten.Domain.Services
{
    public class RitAutoCreatorService : DomainService
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly IRitRepository _ritRepository;

        public RitAutoCreatorService(
            IAggregateFactory aggregateFactory,
            IRitRepository ritRepository)
        {
            _aggregateFactory = aggregateFactory;
            _ritRepository = ritRepository;
        }

        public void AutoCreateRitWhenNeeded(KmStandCreated @event)
        {
            // is deze al gekoppeld aan een rit als eind stand?
            var rit = _ritRepository.FindByKmStandId(@event.Id);
            if (rit == null)
            {
                // zo nee, dan rit aanmaken en de kmstandid koppelen als begin stand
                Rit newRit = _aggregateFactory.Create<Rit>();
                newRit.Create("Generated", @event.Stand, @event.Id, 0, Guid.Empty, Guid.NewGuid());
                _ritRepository.Add(newRit);
            }
        }
    }
}