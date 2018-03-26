using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Ritten.Application.Projections;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Repository;
using Grayson.ExampleCQRS.Ritten.Domain.Services;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;

using Microsoft.Extensions.Logging;

using System;

namespace Grayson.ExampleCQRS.Ritten.Application.Services
{
    public class RitService : ApplicationService,
        IDomainEventHandler<KmStandCreated>
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly KmStandProjectionService _kmStandProjectionService;
        private readonly Func<SeedWork.DDD.Domain.IRepository<KmStand>> _kmStandRepositoryFactory;
        private readonly Func<IKmStandViewRepository> _kmStandViewRepositoryFactory;

        private readonly ILogger _logger;
        private readonly Func<SeedWork.DDD.Domain.IRepository<Rit>> _ritRepositoryFactory;
        private readonly Func<IRitViewRepository> _ritViewRepositoryFactory;

        public RitService(
            ILogger logger,
            IAggregateFactory aggregateFactory,
            Func<IRitViewRepository> ritViewRepositoryFactory,
            Func<IKmStandViewRepository> kmStandViewRepositoryFactory,
            Func<SeedWork.DDD.Domain.IRepository<KmStand>> kmStandRepositoryFactory,
            Func<SeedWork.DDD.Domain.IRepository<Rit>> ritRepositoryFactory,
            KmStandProjectionService kmStandProjectionService)
        {
            _aggregateFactory = aggregateFactory;
            _ritViewRepositoryFactory = ritViewRepositoryFactory;
            _kmStandViewRepositoryFactory = kmStandViewRepositoryFactory;
            _kmStandRepositoryFactory = kmStandRepositoryFactory;
            _ritRepositoryFactory = ritRepositoryFactory;
            _logger = logger;
            _kmStandProjectionService = kmStandProjectionService;
        }

        public void When(KmStandCreated @event)
        {
            _logger.LogInformation($"Recieved event: {nameof(@event)}");
            var ritViewRepository = _ritViewRepositoryFactory();
            var kmStandViewRepository = _kmStandViewRepositoryFactory();
            var ritRepository = _ritRepositoryFactory();
            var kmstandRepository = _kmStandRepositoryFactory();

            // ensure kmstand was been projected
            _kmStandProjectionService.When(@event);

            var domainService = new RitAutoCreatorService(_aggregateFactory, ritViewRepository, kmStandViewRepository, kmstandRepository, ritRepository);
            domainService.AutoCreateRitWhenNeeded(@event);
        }
    }
}