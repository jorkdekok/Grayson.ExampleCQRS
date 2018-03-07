using Grayson.ExampleCQRS.Application.BusinessUseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Grayson.ExampleCQRS.Application.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateNewRitTest()
        {
            var createdNewRit = new CreateNewRit();

            createdNewRit.Handle();
        }

        [TestMethod]
        public void GetRitTest()
        {
            var updateRit = new UpdateRit();

            updateRit.Handle(Guid.Parse("696e6130-0b07-4376-a00a-ba0efe1e2425"));
        }

    }
}
