using AutoFixture;
using AutoFixture.AutoMoq;
using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Ritten.Domain.Test
{
    [TestClass]
    public class RitAutoCreatorServiceTests
    {
        [TestMethod]
        public void Test1()
        {
            // Fixture setup
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var ritRepositoryMock = fixture.Create<IRitRepository>();
            fixture.Inject<IRitRepository>(ritRepositoryMock);

            var sut = fixture.Create<RitAutoCreatorService>();
            sut.AutoCreateRitWhenNeeded(new KmStandCreated(Guid.NewGuid(), 1234, DateTime.Now, Guid.Empty));

        }
    }
}
