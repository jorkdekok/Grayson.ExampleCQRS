using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Grayson.ExampleCQRS.ReadModel.Infrastructure.Repository
{
    public class ReadModelDbContextFactory : IDesignTimeDbContextFactory<ReadModelDbContext>
    {

            public ReadModelDbContext CreateDbContext(string[] args)
            {
            var connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Grayson.Ritm.ReadModel;Integrated Security=True;MultipleActiveResultSets=True";
            var optionsBuilder = new DbContextOptionsBuilder<ReadModelDbContext>();
                optionsBuilder.UseSqlServer(connection);

                return new ReadModelDbContext(optionsBuilder.Options);
            }
    }
}
