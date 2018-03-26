using AutoFixture;
using AutoFixture.AutoMoq;

using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;
using Grayson.ExampleCQRS.Ritten.Domain.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System;
using System.Diagnostics;

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

            var ritRepositoryMock = new Mock<IRitRepository>();
            ritRepositoryMock.Setup(m => m.Add(It.IsAny<Rit>())).Callback<Rit>((r) => { Debug.WriteLine($"{r.Id}"); });

            fixture.Inject<IRitRepository>(ritRepositoryMock.Object);

            var sut = fixture.Create<RitAutoCreatorService>();
            sut.AutoCreateRitWhenNeeded(new KmStandCreated(Guid.NewGuid(), 1234, DateTime.Now, Guid.Empty));
        }
    }
}