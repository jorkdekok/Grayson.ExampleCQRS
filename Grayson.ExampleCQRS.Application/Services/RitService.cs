using System;
using System.Collections.Generic;
using System.Text;
using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Grayson.ExampleCQRS.Domain.Services;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.Services
{
    public class RitService : ApplicationService,
        IDomainEventHandler<KmStandCreated>
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly Func<Utils.DDD.Domain.IRepository<KmStand>> _kmStandRepositoryFactory;
        private readonly Func<IKmStandViewRepository> _kmStandViewRepositoryFactory;
        private readonly Func<Utils.DDD.Domain.IRepository<Rit>> _ritRepositoryFactory;
        private readonly Func<IRitViewRepository> _ritViewRepositoryFactory;

        public RitService(
            IAggregateFactory aggregateFactory,
            Func<IRitViewRepository> ritViewRepositoryFactory,
            Func<IKmStandViewRepository> kmStandViewRepositoryFactory,
            Func<Utils.DDD.Domain.IRepository<KmStand>> kmStandRepositoryFactory,
            Func<Utils.DDD.Domain.IRepository<Rit>> ritRepositoryFactory)
        {
            _aggregateFactory = aggregateFactory;
            _ritViewRepositoryFactory = ritViewRepositoryFactory;
            _kmStandViewRepositoryFactory = kmStandViewRepositoryFactory;
            _kmStandRepositoryFactory = kmStandRepositoryFactory;
            _ritRepositoryFactory = ritRepositoryFactory;
        }

        public void When(KmStandCreated @event)
        {
            //var service = new RitAutoCreatorService(               );
        }
    }
}
