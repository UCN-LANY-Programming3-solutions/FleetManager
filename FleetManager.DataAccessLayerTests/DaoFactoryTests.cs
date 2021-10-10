using FleetManager.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FleetManager.DataAccessLayer.Tests
{
    [TestClass()]
    public class DaoFactoryTests
    {
        [TestMethod()]
        public void CreateCarDaoTest()
        {
            // Arrange
            IDataContext dataContext = Mock.Of<IDataContext>();

            // Act
            IDao<Car> dao = DaoFactory.Create<Car>(dataContext);

            // Assert
            Assert.IsNotNull(dao);
        }

        [TestMethod]
        public void CreateLocationDaoTest()
        {
            // Arrange
            IDataContext dataContext = Mock.Of<IDataContext>();

            // Act
            IDao<Location> dao = DaoFactory.Create<Location>(dataContext);

            // Assert
            Assert.IsNotNull(dao);
        }
    }
}