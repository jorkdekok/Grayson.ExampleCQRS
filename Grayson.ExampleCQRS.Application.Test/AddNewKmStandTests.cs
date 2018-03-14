using System;

using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.Infrastructure.MessageBus;
using Grayson.Utils.DDD.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grayson.ExampleCQRS.Application.Test
{
    [TestClass]
    public class AddNewKmStandTests
    {
        [TestMethod]
        public void AddNewKmStandTest1()
        {
            IMessgeBus bus = new SimpleBus();
            //bus.RegisterHandler<ICommandHandler<AddNewKmStand>, KmStandService>();

            bus.Send(new AddNewKmStand(1000, DateTime.Now, Guid.Empty));
        }
    }
}