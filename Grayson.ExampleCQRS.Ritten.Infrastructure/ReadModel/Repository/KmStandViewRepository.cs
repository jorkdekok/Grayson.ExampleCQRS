using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Repository;

using Microsoft.EntityFrameworkCore.Design;

using System;
using System.Linq;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.ReadModel.Repository
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

        public KmStandView GetPrevious()
        {
            return _context.KmStands.OrderByDescending(k => k.Datum).Skip(1).FirstOrDefault();
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