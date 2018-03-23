using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.ReadModel.Repository
{
    public class ReadModelDbContextFactory : IDesignTimeDbContextFactory<ReadModelDbContext>
    {
        public ReadModelDbContext CreateDbContext(string[] args)
        {
            var connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Grayson.Ritm.Ritten.ReadModel;Integrated Security=True;MultipleActiveResultSets=True";
            var optionsBuilder = new DbContextOptionsBuilder<ReadModelDbContext>();
            optionsBuilder.UseSqlServer(connection);

            return new ReadModelDbContext(optionsBuilder.Options);
        }
    }
}