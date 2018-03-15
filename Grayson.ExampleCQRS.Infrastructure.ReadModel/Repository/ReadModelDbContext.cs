using Grayson.ExampleCQRS.Domain.ReadModel.Model;

using Microsoft.EntityFrameworkCore;

namespace Grayson.ExampleCQRS.Infrastructure.ReadModel.Repository
{
    public class ReadModelDbContext : DbContext
    {
        private const string TABLE_KMSTAND = "KmStand";

        public DbSet<KmStandView> KmStands { get; set; }

        public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Grayson.Ritm.ReadModel;Integrated Security=True;MultipleActiveResultSets=True";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KmStandView>()
                .ToTable(TABLE_KMSTAND)
                .HasKey(e => e.Id);
        }
    }
}