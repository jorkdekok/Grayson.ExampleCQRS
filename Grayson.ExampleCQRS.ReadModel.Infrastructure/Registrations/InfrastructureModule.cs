using System.Linq;

using Grayson.ExampleCQRS.Infrastructure.ReadModel.Repository;

using Microsoft.EntityFrameworkCore.Design;

using SimpleInjector;

namespace Grayson.ExampleCQRS.ReadModel.Infrastructure.Registrations
{
    public static class InfrastructureModule
    {
        public static void RegisterAll(Container container)
        {
            RegisterDbContext(container);
            // repositories
            RegisterRepositories(container);
        }

        public static void RegisterRepositories(Container container)
        {
            var repositoryAssembly = typeof(KmStandViewRepository).Assembly;

            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace == "Grayson.ExampleCQRS.ReadModel.Infrastructure.Repository"
                where type.GetInterfaces().Any()
                select new
                {
                    Service = type.GetInterfaces().Where(i => i.Name.EndsWith("Repository", System.StringComparison.Ordinal)).SingleOrDefault(),
                    Implementation = type
                };

            foreach (var reg in registrations)
            {
                if (reg.Service != null)
                {
                    container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);
                }
            }
        }

        private static void RegisterDbContext(Container container)
        {
            container.Register<IDesignTimeDbContextFactory<ReadModelDbContext>, ReadModelDbContextFactory>();
        }
    }
}