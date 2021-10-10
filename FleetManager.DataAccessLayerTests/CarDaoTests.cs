using FleetManager.DataAccessLayer;
using FleetManager.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.DataAccessLayerTests
{
    [TestClass]
    public class CarDaoTests
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
            IDao<Car> dao = DaoFactory.Create<Car>(dataContext);

            // Act
            IEnumerable<Car> test = dao.ReadAll();

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Count() == 2);
        }

        [TestMethod]
        public void ReadByIdTest()
        {
            //  Arrange
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Car> dao = DaoFactory.Create<Car>(dataContext);

            // Act
            Car test = dao.ReadById(1);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual("Ford", test.Brand);
        }

        [TestMethod]
        public void CreateTest()
        {
            //  Arrange
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Car> dao = DaoFactory.Create<Car>(dataContext);
            Car car = new() { Brand = "Hyundai", Id = 3, Mileage = 32000 };
            
            // Act
            int test = dao.Create(car);

            // Assert
            Assert.AreEqual(1, test);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //  Arrange
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Car> dao = DaoFactory.Create<Car>(dataContext);
            Car car = new() { Id = 1, Brand = "Ford", Mileage = 45000 };

            // Act
            int test = dao.Update(car);

            // Assert
            Assert.AreEqual(1, test);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //  Arrange
            IDataContext dataContext = new DataContext(_connectionString);
            IDao<Car> dao = DaoFactory.Create<Car>(dataContext);
            Car car = new() { Id = 2 };

            // Act
            int test = dao.Delete(car);

            // Assert
            Assert.AreEqual(1, test);
        }
    }
}
