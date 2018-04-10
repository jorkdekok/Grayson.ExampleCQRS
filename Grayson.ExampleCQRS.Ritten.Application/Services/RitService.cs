using Grayson.ExampleCQRS.Ritten.Application.IntegrationEvents;
using Grayson.ExampleCQRS.Ritten.Application.Projections;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Repository;
using Grayson.ExampleCQRS.Ritten.Domain.Services;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.SeedWork.DDD.Domain;

using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Ritten.Application.Services
{
    public class RitService : ApplicationService,
        IIntegrationEventHandler<KmStandCreated>
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly KmStandProjectionService _kmStandProjectionService;
        private readonly Func<IKmStandViewRepository> _kmStandViewRepositoryFactory;

        private readonly ILogger _logger;
        private readonly Func<SeedWork.DDD.Domain.IRepository<Rit>> _ritRepositoryFactory;
        private readonly Func<IRitViewRepository> _ritViewRepositoryFactory;

        public RitService(
            ILogger logger,
            IAggregateFactory aggregateFactory,
            Func<IRitViewRepository> ritViewRepositoryFactory,
            Func<IKmStandViewRepository> kmStandViewRepositoryFactory,
            Func<SeedWork.DDD.Domain.IRepository<Rit>> ritRepositoryFactory,
            KmStandProjectionService kmStandProjectionService)
        {
            _aggregateFactory = aggregateFactory;
            _ritViewRepositoryFactory = ritViewRepositoryFactory;
            _kmStandViewRepositoryFactory = kmStandViewRepositoryFactory;
            _ritRepositoryFactory = ritRepositoryFactory;
            _logger = logger;
            _kmStandProjectionService = kmStandProjectionService;
        }

        public async Task When(KmStandCreated @event)
        {
            _logger.LogInformation($"Recieved event: {@event.GetType().Name}");
            var ritViewRepository = _ritViewRepositoryFactory();
            var kmStandViewRepository = _kmStandViewRepositoryFactory();
            var ritRepository = _ritRepositoryFactory();

            // ensure kmstand was been projected
            await _kmStandProjectionService.When(@event);

            var domainService = new RitAutoCreatorService(_aggregateFactory, ritViewRepository, kmStandViewRepository, ritRepository);
            domainService.AutoCreateRitWhenNeeded(@event.Id, @event.Stand, @event.Datum, @event.AdresId);

            
        }
    }
}