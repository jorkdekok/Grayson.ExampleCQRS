using System;
using System.Linq;

using Grayson.ExampleCQRS.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;

using Microsoft.EntityFrameworkCore.Design;

namespace Grayson.ExampleCQRS.Infrastructure.ReadModel.Repository
{
    public class RitViewRepository : IRitViewRepository
    {
        private readonly ReadModelDbContext _context;
        private readonly IDesignTimeDbContextFactory<ReadModelDbContext> _contextFactory;

        public RitViewRepository(IDesignTimeDbContextFactory<ReadModelDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = contextFactory.CreateDbContext(Array.Empty<string>());
        }

        public void Add(RitView aggregate)
        {
            _context.Ritten.Add(aggregate);
        }

        public void Delete(RitView aggregate)
        {
            _context.Ritten.Remove(aggregate);
        }

        public RitView FindByLastKmStandId(Guid kmstandId)
        {
            return _context.Ritten.Where(r => r.EindStandId == kmstandId).SingleOrDefault();
        }

        public RitView GetById(Guid id)
        {
            return _context.Find(typeof(RitView), id) as RitView;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(RitView aggregate)
        {
            _context.Ritten.Update(aggregate);
        }
    }
}