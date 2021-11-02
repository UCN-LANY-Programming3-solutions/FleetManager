using FleetManager.DataAccessLayer;
using FleetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.DataAccessLayerTests
{
    [TestClass]
    public class LocationDaoTest
    {
        private IDataContext _dataContext;

        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = SqlServerDataContext.Create();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            SqlServerDataContext.Destroy(_dataContext);
        }

        [TestMethod]
        public void ReadAllTest()
        {
            //  Arrange
            IDao<Location> dao = DaoFactory.Create<Location>(_dataContext);

            // Act
            IEnumerable<Location> test = dao.Read();

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Count() == 2);
        }

        [TestMethod]
        public void ReadByIdTest()
        {
            //  Arrange
            IDao<Location> dao = DaoFactory.Create<Location>(_dataContext);

            // Act
            IEnumerable<Location> test = dao.Read(l => l.Id == 1);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.Count());
            Assert.AreEqual("Aalborg", test.Single().Name);
        }


        [TestMethod]
        public void ReadByNonExistingIdTest()
        {
            //  Arrange
            IDao<Location> dao = DaoFactory.Create<Location>(_dataContext);

            // Act
            IEnumerable<Location> testLocation = dao.Read(l => l.Id == 11);

            // Assert
            Assert.IsNotNull(testLocation);
            Assert.AreEqual(0, testLocation.Count());
        }

        [TestMethod]
        public void CreateTest()
        {
            //  Arrange
            IDao<Location> dao = DaoFactory.Create<Location>(_dataContext);
            Location location = new() { Name = "Horsens" };

            // Act
            int test = dao.Create(location);

            // Assert
            Assert.AreEqual(1, test);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //  Arrange
            IDao<Location> dao = DaoFactory.Create<Location>(_dataContext);
            Location location = new() { Id = 1, Name = "Horsens" };

            // Act
            int test = dao.Update(location);

            // Assert
            Assert.AreEqual(1, test);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //  Arrange
            IDao<Location> dao = DaoFactory.Create<Location>(_dataContext);
            Location location = new() { Id = 2 };

            // Act, Assert
            Assert.ThrowsException<DaoException>(() => dao.Delete(location));
        }
    }
}
