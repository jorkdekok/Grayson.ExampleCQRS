using System;
using System.Linq;
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
        }

        public void Delete(KmStandView aggregate)
        {
            _context.KmStands.Remove(aggregate);
        }

        public KmStandView GetById(Guid id)
        {
            return _context.Find(typeof(KmStandView), id) as KmStandView;
        }

        public KmStandView GetLastOne()
        {
            return _context.KmStands.OrderBy(k => k.Datum).LastOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(KmStandView aggregate)
        {
            _context.KmStands.Update(aggregate);
        }
    }
}