using System;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grayson.ExampleCQRS.Ritten.Domain.Test
{
    [TestClass]
    public class RitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Rit rit = new Rit(null);

            //rit.Apply(new RitCreated(Guid.NewGuid(), "test1"));

            Assert.IsNotNull(rit.Id);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Rit rit = new Rit(null);

            ////rit.Create(Guid.NewGuid(), "test1");
            //rit.Create(Guid.NewGuid(), "test2");
            //rit.Create(Guid.NewGuid(), "test3");

            Assert.AreNotEqual(Guid.Empty, rit.Id);
            Assert.AreEqual(3, rit.Version);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Rit rit = new Rit(null);

            //rit.Create(Guid.NewGuid(), "test1");

            Assert.IsNotNull(rit.Id);
        }
    }
}