using System;
using System.Collections.Generic;
using System.Text;
using Grayson.ExampleCQRS.ReadModel.Domain.Model;
using Grayson.ExampleCQRS.ReadModel.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Grayson.ExampleCQRS.ReadModel.Infrastructure.Repository
{
    public class KmStandRepository : IKmStandRepository
    {
        private readonly ReadModelDbContext _context;

        public KmStandRepository()
        {

        }
        public void Add(KmStandView aggregate)
        {
            throw new NotImplementedException();
        }

        public void Delete(KmStandView aggregate)
        {
            throw new NotImplementedException();
        }

        public KmStandView GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(KmStandView aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
