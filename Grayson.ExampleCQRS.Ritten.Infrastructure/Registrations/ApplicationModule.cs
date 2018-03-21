using Grayson.ExampleCQRS.Ritten.Application.Services;
using Grayson.ExampleCQRS.Ritten.Domain.Services;
using Grayson.SeedWork.DDD.Domain;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.Registrations
{
    public static class ApplicationModule
    {
        public static void RegisterAll(Container container)
        {
            RegisterCommandHandlers(container);

            RegisterEventHandlers(container);
        }

        public static void RegisterCommandHandlers(Container container)
        {
            //var typesToRegister = container.GetTypesToRegister(
            //                                typeof(ICommandHandler<>),
            //                                new[] { typeof(KmStandService).Assembly },
            //                                new TypesToRegisterOptions
            //                                {
            //                                    IncludeGenericTypeDefinitions = true,
            //                                    IncludeComposites = false,
            //                                });

            //container.Register(typeof(ICommandHandler<>), typesToRegister);
        }

        public static void RegisterEventHandlers(Container container)
        {
            var typesToRegister = container.GetTypesToRegister(
                                                       typeof(IDomainEventHandler<>),
                                                       new[] { typeof(RitService).Assembly },
                                                       new TypesToRegisterOptions
                                                       {
                                                           IncludeGenericTypeDefinitions = false,
                                                           IncludeComposites = false,
                                                       });

            container.RegisterCollection(typeof(IDomainEventHandler<>), typesToRegister);
        }
    }
}