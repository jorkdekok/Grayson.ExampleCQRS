using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.ReadModel.Repository
{
    public class ReadModelDbContextFactory : IDesignTimeDbContextFactory<ReadModelDbContext>
    {
        private readonly string _sqlDbConnectionAppSetting;

        public ReadModelDbContextFactory(string sqlDbConnectionAppSetting)
        {
            _sqlDbConnectionAppSetting = sqlDbConnectionAppSetting;
        }

        public ReadModelDbContext CreateDbContext(string[] args)
        {
            //var connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Grayson.Ritm.Ritten.ReadModel;Integrated Security=True;MultipleActiveResultSets=True";
            var connection = _sqlDbConnectionAppSetting;
            var optionsBuilder = new DbContextOptionsBuilder<ReadModelDbContext>();
            optionsBuilder.UseSqlServer(connection);

            var context = new ReadModelDbContext(optionsBuilder.Options);
            context.Database.Migrate();
            return context;
        }
    }
}