using System;

using Grayson.ExampleCQRS.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;

using Microsoft.EntityFrameworkCore.Design;

namespace Grayson.ExampleCQRS.Infrastructure.ReadModel.Repository
{
    public class KmStandViewRepository : IKmStandViewRepository
    {
        private readonly ReadModelDbContext _context;
        private readonly IDesignTimeDbContextFactory<ReadModelDbContext> _contextFactory;

        public KmStandViewRepository(IDesignTimeDbContextFactory<ReadModelDbContext> contextFactory)
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