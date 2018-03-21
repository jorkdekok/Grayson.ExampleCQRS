using System;
using System.Threading.Tasks;
using Grayson.ExampleCQRS.KmStanden.Application.Commands;
using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;

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

        public KmStandService(
            IAggregateFactory aggregateFactory,
            IEventPublisher eventPublisher,
            Func<IRepository<KmStand>> repositoryFactory)
        {
            this.aggregateFactory = aggregateFactory;
            _repositoryFactory = repositoryFactory;
            _eventPublisher = eventPublisher;
        }

        public void When(KmStandCreated @event)
        {
        }

        public async Task When(AddNewKmStand command)
        {
            var repository = _repositoryFactory();

            KmStand kmStand = aggregateFactory.Create<KmStand>();
            kmStand.Create(command.Stand, command.Datum, command.AdresId);

            await Task.Run(() => repository.Add(kmStand) );
        }

        public async Task When(UpdateKmStand command)
        {
            //var repository = _repositoryFactory();

            //KmStand kmStand = repository.FindBy(command.Id);

            //kmStand.Update(command.Id, command.Stand, command.Datum, command.AdresId);

            //repository.Save(kmStand);
            this.Update<KmStand>(command.Id,
                _repositoryFactory,
                kmstand => kmstand.Update(command.Id, command.Stand, command.Datum, command.AdresId)
            );
        }
    }
}