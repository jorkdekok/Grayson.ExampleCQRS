using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Application.BusinessUseCases
{
    public class CreateNewRit
    {
        private readonly Func<IRitRepository> _ritRepositoryFactory;

        public CreateNewRit(Func<IRitRepository> ritRepositoryFactory)
        {
            _ritRepositoryFactory = ritRepositoryFactory;
        }

        public CreateNewRit() 
            : this(() => new RitRepository(new EventStore()))
        {
            ///
        }

        public void Handle()
        {
            var repository = _ritRepositoryFactory();

            Rit rit = new Rit();
            rit.Create(Guid.NewGuid(), "test01");

            repository.Add(rit);
        }
    }
}
