using Grayson.ExampleCQRS.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Grayson.ExampleCQRS.ReadModel.Infrastructure.Repository;

using Microsoft.EntityFrameworkCore.Design;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Grayson.ExampleCQRS.Readmodel.Infrastructure.Repository
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

        public IEnumerable<KmStandView> GetAll(int page, int pageSize)
        {
            var list = _context.KmStands.Skip(page * pageSize).Take(pageSize).ToList();
            return list;
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