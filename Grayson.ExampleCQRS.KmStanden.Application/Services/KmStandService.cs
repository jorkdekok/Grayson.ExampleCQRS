using System;
using System.Threading.Tasks;
using Grayson.ExampleCQRS.KmStanden.Application.Commands;
using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;
using Microsoft.Extensions.Logging;

namespace Grayson.ExampleCQRS.KmStanden.Application.Services
{
    public class KmStandService : ApplicationService,
        ICommandHandler<AddNewKmStand>,
        ICommandHandler<UpdateKmStand>,
        IDomainEventHandler<KmStandCreated>
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly Func<IRepository<KmStand>> _repositoryFactory;
        private readonly IAggregateFactory aggregateFactory;
        private readonly ILogger _logger;

        public KmStandService(
            ILogger logger,
            IAggregateFactory aggregateFactory,
            IEventPublisher eventPublisher,
            Func<IRepository<KmStand>> repositoryFactory)
        {
            this.aggregateFactory = aggregateFactory;
            _repositoryFactory = repositoryFactory;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public void When(KmStandCreated @event)
        {
        }

        public async Task When(AddNewKmStand command)
        {
            _logger.LogInformation($"received command: {command.GetType().Name}");
            var repository = _repositoryFactory();

            KmStand kmStand = aggregateFactory.Create<KmStand>();
            kmStand.Create(command.Stand, command.Datum, command.AdresId);

            await Task.Run(() => repository.Add(kmStand) );
        }

        public async Task When(UpdateKmStand command)
        {
            _logger.LogInformation($"received command: {command.GetType().Name}");
            await this.Update<KmStand>(command.Id,
                _repositoryFactory,
                kmstand => kmstand.Update(command.Id, command.Stand, command.Datum, command.AdresId)
            );
        }
    }
}