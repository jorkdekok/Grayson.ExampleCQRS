using Grayson.ExampleCQRS.ReadModel.Domain.Model;

using Microsoft.EntityFrameworkCore;

namespace Grayson.ExampleCQRS.ReadModel.Infrastructure.Repository
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
            var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KmStandView>().ToTable(TABLE_KMSTAND);
        }
    }
}