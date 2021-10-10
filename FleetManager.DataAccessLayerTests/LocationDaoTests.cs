using FleetManager.DataAccessLayer;
using FleetManager.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.DataAccessLayerTests
{
    [TestClass]
    public class LocationDaoTest
    {
        private readonly string _connectionString = @$"Data Source=(localdb)\mssqllocaldb; Initial Catalog=FleetManager_test_{Guid.NewGuid()}; Integrated Security=true";

        [TestInitialize]
        public void InitializeTest()
        {
            Database.Version.Upgrade(_connectionString);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Database.Version.Drop(_connectionString);
        }

        [TestMethod]
        public void ReadAllTest()
        {
            //  Arrange
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Location> dao = DaoFactory.Create<Location>(dataContext);

            // Act
            IEnumerable<Location> test = dao.ReadAll();

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Count() == 2);
        }

        [TestMethod]
        public void ReadByIdTest()
        {
            //  Arrange
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Location> dao = DaoFactory.Create<Location>(dataContext);

            // Act
            Location test = dao.ReadById(1);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual("Aalborg", test.Name);
        }

        [TestMethod]
        public void CreateTest()
        {
            //  Arrange
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Location> dao = DaoFactory.Create<Location>(dataContext);
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
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Location> dao = DaoFactory.Create<Location>(dataContext);
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
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Location> dao = DaoFactory.Create<Location>(dataContext);
            Location location = new() { Id = 2 };

            // Act, Assert
            Assert.ThrowsException<DaoException>(() => dao.Delete(location));
        }
    }
}
