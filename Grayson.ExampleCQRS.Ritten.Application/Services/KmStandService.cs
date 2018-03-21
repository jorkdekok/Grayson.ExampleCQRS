using System;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.SeedWork.DDD.Application;
using Grayson.SeedWork.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.Services
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

        public void When(AddNewKmStand command)
        {
            var repository = _repositoryFactory();

            KmStand kmStand = aggregateFactory.Create<KmStand>();
            kmStand.Create(command.Stand, command.Datum, command.AdresId);

            repository.Add(kmStand);
        }

        public void When(UpdateKmStand command)
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