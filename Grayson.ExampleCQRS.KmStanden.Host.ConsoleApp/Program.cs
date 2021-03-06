﻿using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.ExampleCQRS.KmStanden.Infrastructure.Registrations;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.Utils.Configuration;
using Grayson.Utils.Logging;
using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SimpleInjector;

using System;
using System.IO;

namespace Grayson.ExampleCQRS.KmStanden.Host.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();
                // configuration appsettings convention
                IConfiguration configuration = new ConfigurationBuilder()
                    
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
                container.Options.RegisterParameterConvention(new AppSettingsConvention(key => configuration[key]));

                ILoggerFactory loggerFactory = new LoggerFactory()
                    .AddConsole()
                    .AddDebug();

                container.Options.DependencyInjectionBehavior = new MsContextualLoggerInjectionBehavior(loggerFactory, container);

                ILogger logger = loggerFactory.CreateLogger<Program>();
                //container.RegisterSingleton<ILogger>(logger);
                logger.LogInformation("Starting BC 'KmStanden' host...");

                ExampleCQRS.Infrastructure.Registrations.InfrastructureModule.RegisterEventBus(container, configuration);

                DomainModule.RegisterAll(container);
                ApplicationModule.RegisterAll(container);
                InfrastructureModule.RegisterAll(container);
                InfrastructureModule.RegisterEventForwarder(container);
                RabbitMqModule.RegisterCommandConsumers(container);

                var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(configuration["CommandBusConnection"]), hst =>
                    {
                        hst.Username(configuration["CommandBusUserName"]);
                        hst.Password(configuration["CommandPassword"]);
                    });

                    cfg.SeparatePublishFromSendTopology();
                    // command queue
                    cfg.ReceiveEndpoint(host,
                        RabbitMqConstants.CommandsQueue, e =>
                        {
                            e.LoadFrom(container);
                        });
                });

                container.RegisterSingleton(bus);

                //container.RegisterSingleton(RabbitMqConfiguration.ConfigureBus((cfg, host) =>
                //{
                //    cfg.SeparatePublishFromSendTopology();
                //    // command queue
                //    cfg.ReceiveEndpoint(host,
                //        RabbitMqConstants.CommandsQueue, e =>
                //        {
                //            e.LoadFrom(container);
                //        });
                //}));

                using (var eventBus = container.GetInstance<IIntegrationEventBus>())
                {
                    var cbus = container.GetInstance<IBusControl>();

                    cbus.StartAsync();

                    Console.WriteLine("Listening for commands.. Press enter to exit");
                    Console.ReadLine();

                    cbus.StopAsync();
                }
            }
        }
    }
}