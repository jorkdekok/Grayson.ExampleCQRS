﻿using System;
using System.Collections.Generic;
using System.Text;
using Grayson.ExampleCQRS.Application.Services;
using Grayson.Utils.DDD.Application;
using Grayson.Utils.DDD.Domain;
using Grayson.Utils.DDD.Infrastructure;
using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.Registrations
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
            container.Options.AllowOverridingRegistrations = true;

            var typesToRegister = container.GetTypesToRegister(
                                            typeof(ICommandHandler<>),
                                            new[] { typeof(KmStandService).Assembly },
                                            new TypesToRegisterOptions
                                            {
                                                IncludeGenericTypeDefinitions = true,
                                                IncludeComposites = false,
                                            });

            container.Register(typeof(ICommandHandler<>), typesToRegister);
        }

        public static void RegisterEventHandlers(Container container)
        {
            container.Options.AllowOverridingRegistrations = true;

            var typesToRegister = container.GetTypesToRegister(
                                                       typeof(IDomainEventHandler<>),
                                                       new[] { typeof(KmStandService).Assembly },
                                                       new TypesToRegisterOptions
                                                       {
                                                           IncludeGenericTypeDefinitions = false,
                                                           IncludeComposites = false,
                                                       });

            container.RegisterCollection(typeof(IDomainEventHandler<>), typesToRegister);
        }

       
    }
}
