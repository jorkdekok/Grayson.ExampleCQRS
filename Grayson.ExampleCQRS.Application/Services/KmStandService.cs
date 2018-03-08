﻿using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.Utils.DDD;
using System;

namespace Grayson.ExampleCQRS.Application.Services
{
    public class KmStandService : ApplicationService, ICommandHandler<AddNewKmStand>
    {
        private readonly Func<IRepository<KmStand>> _repositoryFactory;

        private KmStandService(Func<IRepository<KmStand>> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public void When(AddNewKmStand command)
        {
            var repository = _repositoryFactory();

            KmStand kmStand = new KmStand();
            kmStand.Create(command.Stand, command.Datum, command.AdresId);

            repository.Add(kmStand);
        }
    }
}