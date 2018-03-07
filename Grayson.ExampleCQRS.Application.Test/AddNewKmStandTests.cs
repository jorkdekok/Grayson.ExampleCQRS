using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Application.Services;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Grayson.ExampleCQRS.Application.Test
{
    [TestClass]
    public class AddNewKmStandTests
    {
        [TestMethod]
        public void AddNewKmStandTest1()
        {
            IBus bus = new SimpleBus();
            bus.RegisterHandler<KmStandService>();

            bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));
        }
    }
}