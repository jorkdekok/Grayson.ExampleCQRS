using System;
using System.Collections.Generic;
using System.Text;
using Grayson.ExampleCQRS.ReadModel.Domain.Model;
using Grayson.ExampleCQRS.ReadModel.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Grayson.ExampleCQRS.ReadModel.Infrastructure.Repository
{
    public class KmStandRepository : IKmStandRepository
    {
        private readonly IDesignTimeDbContextFactory<ReadModelDbContext> _contextFactory;
        private readonly ReadModelDbContext _context;

        public KmStandRepository(IDesignTimeDbContextFactory<ReadModelDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = contextFactory.CreateDbContext(Array.Empty<string>());
        }

        public void Add(KmStandView aggregate)
        {
            _context.KmStands.Add(aggregate);
            _context.SaveChanges();
        }

        public void Delete(KmStandView aggregate)
        {
            throw new NotImplementedException();
        }

        public KmStandView GetById(Guid id)
        {
            return _context.Find(typeof(KmStandView), id) as KmStandView;
        }

        public void Update(KmStandView aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
