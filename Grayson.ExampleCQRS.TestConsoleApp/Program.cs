﻿using System;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Infrastructure.Extensions;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD.Application;

using SimpleInjector;

namespace Grayson.ExampleCQRS.TestConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new Container())
            {
                container.Options.AllowResolvingFuncFactories();

                MessageBusRegistrations.Register(container);

                container.RegisterSingleton(AdvancedBus.ConfigureBus());

                var bus = container.GetInstance<ICommandBus>();

                bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));
                Console.WriteLine("AddNewKmStand Command was send");

                bus.Send(new UpdateKmStand(Guid.Parse("d6a8eb8e-690a-4eea-94ab-d300458c4b10"),2000, DateTime.Now, Guid.Empty));
                Console.WriteLine("UpdateKmStand Command was send");

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }
    }
}