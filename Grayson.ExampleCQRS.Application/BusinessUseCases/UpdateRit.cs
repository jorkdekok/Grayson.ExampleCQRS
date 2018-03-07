using Grayson.ExampleCQRS.Domain.Model;
using Grayson.ExampleCQRS.Domain.Repository;
using Grayson.ExampleCQRS.Infrastructure.EventSourcing;
using Grayson.ExampleCQRS.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Application.BusinessUseCases
{
    public class UpdateRit
    {
        private readonly Func<IRepository<Rit>> _ritRepositoryFactory;

        public UpdateRit(Func<IRepository<Rit>> ritRepositoryFactory)
        {
            _ritRepositoryFactory = ritRepositoryFactory;
        }

        public UpdateRit()
            : this(() => new Repository<Rit>(new EventStore()))
        {
            ///
        }

        public void Handle(Guid id)
        {
            var repository = _ritRepositoryFactory();

            Rit rit = repository.FindBy(id);

            rit.Update(id, "test02");

            repository.Save(rit);
        }
    }
}
