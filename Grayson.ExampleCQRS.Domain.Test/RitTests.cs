using Grayson.ExampleCQRS.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Grayson.ExampleCQRS.Domain.Test
{
    [TestClass]
    public class RitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Rit rit = new Rit();

            rit.Apply(new RitCreated(Guid.NewGuid(), "test1"));

            Assert.IsNotNull(rit.Id);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Rit rit = new Rit();

            rit.Create(Guid.NewGuid(), "test1");
            rit.Create(Guid.NewGuid(), "test2");
            rit.Create(Guid.NewGuid(), "test3");

            Assert.AreNotEqual(Guid.Empty, rit.Id);
            Assert.AreEqual(3, rit.Version);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Rit rit = new Rit();

            rit.Create(Guid.NewGuid(), "test1");
            
            Assert.IsNotNull(rit.Id);
        }
    }
}
