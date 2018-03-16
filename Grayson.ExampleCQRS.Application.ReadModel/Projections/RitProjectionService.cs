﻿using Grayson.ExampleCQRS.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Application.ReadModel.Projections
{
    public class RitProjectionService : ApplicationService,
        IDomainEventHandler<RitCreated>,
        IDomainEventHandler<RitUpdated>
    {
        private readonly IKmStandViewRepository _kmStandRepository;
        private readonly IRitViewRepository _ritViewRepository;

        public RitProjectionService(IKmStandViewRepository kmStandRepository, IRitViewRepository ritViewRepository)
        {
            _kmStandRepository = kmStandRepository;
            _ritViewRepository = ritViewRepository;
        }

        public void When(KmStandCreated @event)
        {
            var kmstandView = _kmStandRepository.GetById(@event.Id);
            if (kmstandView == null)
            {
                _kmStandRepository.Add(new KmStandView() { Id = @event.Id, AdresId = @event.AdresId, Datum = @event.Datum, Stand = @event.Stand });
                _kmStandRepository.SaveChanges();
            }
        }

        public void When(KmStandUpdated @event)
        {
            var kmstandView = _kmStandRepository.GetById(@event.Id);
            if (kmstandView != null)
            {
                kmstandView.AdresId = @event.AdresId;
                kmstandView.Datum = @event.Datum;
                kmstandView.Stand = @event.Stand;

                _kmStandRepository.SaveChanges();
            }
        }

        public void When(RitUpdated @event)
        {
            var ritView = _ritViewRepository.GetById(@event.Id);
            if (ritView != null)
            {
                ritView.Id = @event.Id;
                ritView.BeginStand = @event.BeginStand;
                ritView.BeginStandId = @event.BeginStandId;
                ritView.EindStand = @event.EindStand;
                ritView.EindStandId = @event.EindStandId;
                ritView.Name = @event.Name;
                _ritViewRepository.SaveChanges();
            }
        }

        public void When(RitCreated @event)
        {
            var ritView = _ritViewRepository.GetById(@event.Id);
            if (ritView == null)
            {
                _ritViewRepository.Add(new RitView()
                {
                    Id = @event.Id,
                    BeginStand = @event.BeginStand,
                    BeginStandId = @event.BeginStandId,
                    EindStand = @event.EindStand,
                    EindStandId = @event.EindStandId,
                    Name = @event.Name
                });
                _ritViewRepository.SaveChanges();
            }
        }
    }
}