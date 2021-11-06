using FleetManager.Model;
using Lanysom.DataContextBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FleetManager.DataAccessLayer.Tests.UnitTests
{
    [TestClass]
    public class MemoryDataContextBuilderTests
    {
        [TestMethod]
        public void ShouldBuildMemoryDataContext()
        {
            IDataContext dataContext = DataContextBuilder.For
                .Memory()
                .Initialize<MemoryDataContext>()
                .Feed<Location>("locations.json")
                .Feed<Car>("cars.json")
                .Build<IDataContext>();

            Assert.IsNotNull(dataContext);
            Assert.IsInstanceOfType(dataContext, typeof(MemoryDataContext));
            Assert.AreEqual(2, ((MemoryDataContext)dataContext).Locations.Count);
            Assert.AreEqual(2, ((MemoryDataContext)dataContext).Cars.Count);
        }
    }
}
